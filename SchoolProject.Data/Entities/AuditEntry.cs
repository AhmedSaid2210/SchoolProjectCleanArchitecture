using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SchoolProject.Data.Entities
{
    public class AuditEntry
    {
        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }

        public EntityEntry Entry { get; }

        public string TableName { get; set; } = string.Empty;

        public string Action { get; set; } = string.Empty;

        public string? UserName { get; set; }

        public Dictionary<string, object?> KeyValues { get; } = new();

        public Dictionary<string, object?> OldValues { get; } = new();

        public Dictionary<string, object?> NewValues { get; } = new();

        public List<string> ChangedColumns { get; } = new();

        public AuditLog ToAuditLog()
        {
            return new AuditLog
            {
                TableName = TableName,
                Action     = Action,
                UserName   = UserName,
                ActionDate = DateTime.UtcNow,
                PrimaryKey = System.Text.Json.JsonSerializer.Serialize(KeyValues),
                OldValues  = System.Text.Json.JsonSerializer.Serialize(OldValues),
                NewValues  = System.Text.Json.JsonSerializer.Serialize(NewValues),
                ChangedColumns = System.Text.Json.JsonSerializer.Serialize(ChangedColumns)
            };
        }
    }
}
