using Convey.MessageBrokers;
using ECommerce.Services.Discounts.Core.DTO;
using ECommerce.Services.Discounts.Core.Entities;
using ECommerce.Services.Discounts.Core.Events;
using ECommerce.Services.Discounts.Core.Exceptions;
using ECommerce.Services.Discounts.Core.Repositories;
using ECommerce.Services.Discounts.Core.Validators;

namespace ECommerce.Services.Discounts.Core.Services;

internal class DiscountCodeService : IDiscountCodeService
{
    private readonly IBusPublisher _busPublisher;
    private readonly DiscountDateValidator _dateValidator;
    private readonly IDiscountCodeRepository _discountCodeRepository;
    private readonly IProductRepository _productRepository;

    public DiscountCodeService(IDiscountCodeRepository discountCodeRepository, IProductRepository productRepository,
        DiscountDateValidator dateValidator, IBusPublisher busPublisher)
    {
        _discountCodeRepository = discountCodeRepository;
        _productRepository = productRepository;
        _dateValidator = dateValidator;
        _busPublisher = busPublisher;
    }

    public async Task AddAsync(DiscountCodeDto dto)
    {
        List<Product> products = new();
        if (dto.ProductIds.Any())
        {
            products = (await _productRepository.GetAsync(dto.ProductIds)).ToList();

            if (products.Count != dto.ProductIds.Count) throw new ProductsNotFoundException();
        }

        var isDateValid = _dateValidator.Validate(dto.ValidFrom, dto.ValidTo);

        if (!isDateValid) throw new InvalidDiscountDateException();

        var exists = await _discountCodeRepository.ExistsAsync(dto.Code);

        if (exists) throw new DiscountCodeAlreadyExistsException(dto.Code);

        dto.Id = Guid.NewGuid();
        var discountCode = new DiscountCode
        {
            Id = dto.Id,
            Code = dto.Code,
            Description = dto.Description,
            Percentage = dto.Percentage,
            Products = products.Count == 0 ? null : products,
            ValidFrom = dto.ValidFrom,
            ValidTo = dto.ValidTo
        };
        await _discountCodeRepository.AddAsync(discountCode);
    }

    public async Task DeleteAsync(Guid id)
    {
        var discountCode = await _discountCodeRepository.GetAsync(id);

        if (discountCode is null) throw new DiscountCodeNotFoundException(id);

        await _discountCodeRepository.DeleteAsync(discountCode);
        await _busPublisher.PublishAsync(new DiscountCodeExpired(id));
    }
}