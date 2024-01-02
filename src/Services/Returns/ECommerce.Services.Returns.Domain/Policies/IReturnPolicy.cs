using ECommerce.Services.Returns.Domain.Entities;
using ECommerce.Shared.Abstractions.Time;

namespace ECommerce.Services.Returns.Domain.Policies;

public interface IReturnPolicy
{
    void Return(Return @return, IClock clock);
}