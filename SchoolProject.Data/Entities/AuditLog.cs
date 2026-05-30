
namespace SchoolProject.Data.Entities
{
    public class AuditLog
    {
        public long Id { get; set; }

        public string TableName { get; set; } = string.Empty;

        public string Action { get; set; } = string.Empty;

        public string? PrimaryKey { get; set; }

        public string? UserName { get; set; }

        public DateTime ActionDate { get; set; }

        public string? OldValues { get; set; }

        public string? NewValues { get; set; }

        public string? ChangedColumns { get; set; }
    }
}
