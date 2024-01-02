using ECommerce.Shared.Abstractions.Time;

namespace ECommerce.Services.Discounts.Core.Validators;

internal sealed class DiscountDateValidator
{
    private readonly IClock _clock;

    public DiscountDateValidator(IClock clock)
    {
        _clock = clock;
    }

    public bool Validate(DateTime from, DateTime to)
    {
        if (from < _clock.CurrentDate()) return false;

        if (to < from) return false;

        return true;
    }
}