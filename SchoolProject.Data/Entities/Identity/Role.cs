

using Microsoft.AspNetCore.Identity;

namespace SchoolProject.Data.Entities.Identity
{
    public class Role:IdentityRole<int>
    {
        public DateTime CreatedOn { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public string? LastModifiedBy { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string? DeletedBy { get; set; }
    }
}
