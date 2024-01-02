using ECommerce.Shared.Abstractions.Time;

namespace ECommerce.Modules.Orders.UnitTests.Shared.Time;

public class TestClock : IClock
{
    public DateTime CurrentDate()
    {
        return new DateTime(2023, 9, 1);
    }
}