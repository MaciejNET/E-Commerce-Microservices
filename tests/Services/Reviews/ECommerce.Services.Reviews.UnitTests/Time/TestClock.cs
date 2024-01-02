using ECommerce.Shared.Abstractions.Time;

namespace ECommerce.Services.Reviews.UnitTests.Time;

public class TestClock : IClock
{
    public DateTime CurrentDate()
    {
        return new DateTime(2023, 8, 10, 12, 0, 0);
    }
}