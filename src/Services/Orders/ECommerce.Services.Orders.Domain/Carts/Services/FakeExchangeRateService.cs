using ECommerce.Shared.Abstractions.Kernel.Enums;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Services.Orders.Domain.Carts.Services;

internal sealed class FakeExchangeRateService : IExchangeRateService
{
    private readonly Dictionary<Currency, decimal> _exchangeRate = new()
    {
        {Currency.PLN, 1.00M}, // 1 PLN to PLN
        {Currency.USD, 3.95M}, // Fake data
        {Currency.EUR, 4.27M}, // Fake data
        {Currency.GBP, 4.98M} // Fake data
    };

    public async Task<Price> Exchange(Price price, Currency currency)
    {
        if (price == null) throw new ArgumentNullException(nameof(price));

        if (currency == null) throw new ArgumentNullException(nameof(currency));

        if (price.Currency == currency) return price;

        await Task.Delay(TimeSpan.FromMilliseconds(new Random().Next(100, 1000)));

        var exchangeRate = GetExchangeRate(price.Currency, currency);

        var newAmount = price.Amount * exchangeRate;

        var newPrice = new Price(newAmount, currency);

        return newPrice;
    }

    private decimal GetExchangeRate(Currency fromCurrency, Currency toCurrency)
    {
        if (!_exchangeRate.TryGetValue(fromCurrency, out var fromRate) ||
            !_exchangeRate.TryGetValue(toCurrency, out var toRate))
            throw new ArgumentOutOfRangeException("Exchange rate not available for one or both currencies.");

        var randomFactor = (decimal) (new Random().NextDouble() * 0.2) + 0.9M;

        return toRate / fromRate * randomFactor;
    }
}