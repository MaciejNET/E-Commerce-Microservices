using System.ComponentModel.DataAnnotations;

namespace ECommerce.Services.Users.Core.DTO;

internal class SignInDto
{
    [EmailAddress] [Required] public string Email { get; set; }

    [Required] public string Password { get; set; }
}