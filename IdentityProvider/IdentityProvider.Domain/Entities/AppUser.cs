using IdentityProvider.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityProvider.Domain.Entities
{
    public class AppUser : IdentityUser, IEntity
    {
        public string CreatedByUserId { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.MinValue;
        public string? ModifiedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
