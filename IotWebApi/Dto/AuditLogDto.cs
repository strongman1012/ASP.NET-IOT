using MongoDB.Bson;
using System.Text.Json.Serialization;
namespace IotWebApi.Dto
{
    public class AuditLogDto
    {
        public string Id { get; set; } = string.Empty;
        public DateTime? Time { get; set; } = DateTime.Now;
        public string UserId { get; set; }
        public string? AffectedUser { get; set; }
        public string? EventContext { get; set; }
        public string? EventName { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
    }
}
