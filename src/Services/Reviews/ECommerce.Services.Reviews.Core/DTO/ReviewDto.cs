using System.ComponentModel.DataAnnotations;

namespace ECommerce.Services.Reviews.Core.DTO;

internal class ReviewDto
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; }

    [EmailAddress] public string Email { get; set; }

    public string Content { get; set; }

    [Range(1, 5)] public int Rating { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}