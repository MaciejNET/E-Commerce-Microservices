using ECommerce.Shared.Abstractions.Kernel.Enums;

namespace ECommerce.Services.Users.Core.DTO;

internal class AccountDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public Currency PreferedCurrency { get; set; }
    public Dictionary<string, IEnumerable<string>> Claims { get; set; }
    public DateTime CreatedAt { get; set; }
}