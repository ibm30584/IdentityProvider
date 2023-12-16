namespace IdentityProvider.Domain.Models
{
    public interface IEntity
    {
        string CreatedByUserId { get; set; }
        DateTime CreatedDate { get; set; }
        string? ModifiedByUserId { get; set; }
        DateTime? ModifiedDate { get; set; }
    }
}
