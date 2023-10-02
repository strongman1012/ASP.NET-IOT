using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
namespace IotWebApi.Entities
{
    public class AuditLogEto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public DateTime? Time { get; set; } = DateTime.Now;
        public string UserId { get; set; }
        public string? AffectedUser { get; set; }
        public string? EventContext { get; set; }
        public string? EventName { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
    }
}
