namespace ECommerce.Services.Reviews.Core.Entities;

internal class Review
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public Guid? UserId { get; set; }
    public Guid ProductId { get; set; }
    public string Content { get; set; }
    public int Rating { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}