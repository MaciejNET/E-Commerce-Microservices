using ECommerce.Shared.Abstractions.Time;

namespace ECommerce.Services.Discounts.UnitTests.Time;

public class TestClock : IClock
{
    public DateTime CurrentDate()
    {
        return new DateTime(2023, 8, 15);
    }
}