using Convey.MessageBrokers;
using ECommerce.Services.Discounts.Core.DTO;
using ECommerce.Services.Discounts.Core.Entities;
using ECommerce.Services.Discounts.Core.Events;
using ECommerce.Services.Discounts.Core.Exceptions;
using ECommerce.Services.Discounts.Core.Repositories;
using ECommerce.Services.Discounts.Core.Validators;

namespace ECommerce.Services.Discounts.Core.Services;

internal class ProductDiscountService : IProductDiscountService
{
    private readonly IBusPublisher _busPublisher;
    private readonly DiscountDateValidator _dateValidator;
    private readonly IProductDiscountRepository _productDiscountRepository;
    private readonly IProductRepository _productRepository;

    public ProductDiscountService(IProductDiscountRepository productDiscountRepository,
        IProductRepository productRepository, DiscountDateValidator dateValidator, IBusPublisher busPublisher)
    {
        _productDiscountRepository = productDiscountRepository;
        _productRepository = productRepository;
        _dateValidator = dateValidator;
        _busPublisher = busPublisher;
    }

    public async Task AddAsync(ProductDiscountDto dto)
    {
        var product = await _productRepository.GetAsync(dto.ProductId);

        if (product is null) throw new ProductNotFoundException(dto.ProductId);

        var isDateValid = _dateValidator.Validate(dto.ValidFrom, dto.ValidTo);

        if (!isDateValid) throw new InvalidDiscountDateException();

        var canAddDiscountForProduct =
            await _productDiscountRepository.CanAddDiscountForProductAsync(dto.ProductId, dto.ValidFrom, dto.ValidTo);

        if (!canAddDiscountForProduct) throw new ProductAlreadyHasDiscountException(dto.ProductId);

        dto.Id = Guid.NewGuid();
        var productDiscount = new ProductDiscount
        {
            Id = dto.Id,
            NewPrice = dto.NewPrice,
            ProductId = product.Id,
            Product = product,
            ValidFrom = dto.ValidFrom,
            ValidTo = dto.ValidTo
        };
        await _productDiscountRepository.AddAsync(productDiscount);
    }

    public async Task DeleteAsync(Guid id)
    {
        var productDiscount = await _productDiscountRepository.GetAsync(id);

        if (productDiscount is null) throw new ProductDiscountNotFoundException(id);

        await _productDiscountRepository.DeleteAsync(productDiscount);
        await _busPublisher.PublishAsync(new ProductDiscountExpired(productDiscount.ProductId));
    }
}