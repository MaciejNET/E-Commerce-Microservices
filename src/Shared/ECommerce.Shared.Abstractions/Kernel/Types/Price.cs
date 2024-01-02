using ECommerce.Shared.Abstractions.Kernel.Enums;
using ECommerce.Shared.Abstractions.Kernel.Exceptions;

namespace ECommerce.Shared.Abstractions.Kernel.Types;

public record Price
{
    public decimal Amount { get; private set; }
    public Currency Currency { get; private set; }

    public Price(decimal amount, Currency currency)
    {
        if (amount < 0)
        {
            throw new InvalidPriceException();
        }
        
        Amount = amount;
        Currency = currency;
    }
}