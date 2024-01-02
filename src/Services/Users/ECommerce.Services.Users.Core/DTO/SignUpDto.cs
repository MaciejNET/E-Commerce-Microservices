using System.ComponentModel.DataAnnotations;
using ECommerce.Shared.Abstractions.Kernel.Enums;

namespace ECommerce.Services.Users.Core.DTO;

internal class SignUpDto
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string FullName { get; set; }

    [EmailAddress] [Required] public string Email { get; set; }

    [Required] public string Password { get; set; }

    public string Role { get; set; }

    public Currency PreferredCurrency { get; set; } = Currency.PLN;

    public Dictionary<string, IEnumerable<string>> Claims { get; set; }
}