using ECommerce.Shared.Abstractions.Kernel.Enums;

namespace ECommerce.Services.Users.Core.Entities;

internal class User
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public bool IsActive { get; set; }
    public Currency PreferredCurrency { get; set; } = Currency.PLN;
    public DateTime CreatedAt { get; set; }
    public Dictionary<string, IEnumerable<string>> Claims { get; set; }
}