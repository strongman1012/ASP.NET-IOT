using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace IotWebApi.Entities
{
    public abstract class BaseEto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public DateTime? DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateModified { get; set; } = DateTime.Now;
    }
}
