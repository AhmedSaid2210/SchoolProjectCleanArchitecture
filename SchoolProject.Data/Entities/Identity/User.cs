

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities.Identity
{
    public class User : IdentityUser<int>
    {
        public User()
        {
            userRefreshToken = new HashSet<UserRefreshToken>();
        }
        public string FullName { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        [InverseProperty(nameof(UserRefreshToken.user))]
        public virtual ICollection<UserRefreshToken> userRefreshToken { get; set; }
        public DateTime CreatedOn { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public string? LastModifiedBy { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string? DeletedBy { get; set; }
    }
}
