using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helper;
using System.Reflection;
using System.Security.Claims;


namespace SchoolProject.Infrustructure.Data
{
    public class AppDbContext : IdentityDbContext<User,
                                                IdentityRole<int>,
                                                int,
                                                IdentityUserClaim<int>,
                                                IdentityUserRole<int>,
                                                IdentityUserLogin<int>,
                                                IdentityRoleClaim<int>,
                                                IdentityUserToken<int>>
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
      
        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        private string CurrentUser =>
         _httpContextAccessor.HttpContext?.User?
        .FindFirst(nameof(UserClaimModel.UserName))?.Value
        ?? "System";

        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }

        public DbSet<Subjects> Subjects { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<DepartmetSubject> DepartmetSubjects { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<UserRefreshToken> UserRefreshToken { get; set; }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var auditEntries = OnBeforeSaveChanges();

            var result = await base.SaveChangesAsync(cancellationToken);

            await OnAfterSaveChangesAsync(auditEntries, cancellationToken);

            return result;
        }
        private async Task OnAfterSaveChangesAsync(List<AuditEntry> auditEntries,CancellationToken cancellationToken)
        {
            if (!auditEntries.Any())
                return;

            var logs = auditEntries.Select(x => x.ToAuditLog()).ToList();

            AuditLogs.AddRange(logs);

            await base.SaveChangesAsync(cancellationToken);
        }

        private List<AuditEntry> OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();

            var auditEntries = new List<AuditEntry>();

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditLog)
                    continue;

                if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditEntry(entry);

                auditEntry.TableName = entry.Metadata.GetTableName()!;

                auditEntry.UserName = CurrentUser;

                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;

                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
    }

                    switch (entry.State)
                    {
                        case EntityState.Added:

                            auditEntry.Action = "INSERT";

                            auditEntry.NewValues[propertyName] = property.CurrentValue;

                            break;

                        case EntityState.Modified:

                            if (!property.IsModified)
                                continue;

                            auditEntry.Action = "UPDATE";

                            auditEntry.ChangedColumns.Add(propertyName);

                            auditEntry.OldValues[propertyName] = property.OriginalValue;

                            auditEntry.NewValues[propertyName] = property.CurrentValue;

                            break;

                        case EntityState.Deleted:

                            auditEntry.Action = "DELETE";

                            auditEntry.OldValues[propertyName] = property.OriginalValue;

                            break;
                    }
                }
                if (entry.Entity is AuditableEntity auditable)
                {
                    
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            
                            auditable.CreatedOn = DateTime.UtcNow;

                            auditable.CreatedBy = CurrentUser;

                            break;

                        case EntityState.Modified:

                            auditable.LastModifiedOn = DateTime.UtcNow;

                            auditable.LastModifiedBy = CurrentUser;
                            break;

                        case EntityState.Deleted:

                            entry.State = EntityState.Modified;

                            auditable.IsDeleted = true;

                            auditable.DeletedOn = DateTime.UtcNow;

                            auditable.DeletedBy = CurrentUser;

                            break;
                    }
                }

                auditEntries.Add(auditEntry);
            }

            return auditEntries;
        }
    }
    


 
}
