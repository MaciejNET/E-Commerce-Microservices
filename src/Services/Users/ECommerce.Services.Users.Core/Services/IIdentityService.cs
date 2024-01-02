using ECommerce.Services.Users.Core.DTO;
using ECommerce.Shared.Abstractions.Auth;

namespace ECommerce.Services.Users.Core.Services;

internal interface IIdentityService
{
    Task<AccountDto> GetAsync(Guid id);
    Task<JsonWebToken> SignInAsync(SignInDto dto);
    Task SignUpAsync(SignUpDto dto);
}