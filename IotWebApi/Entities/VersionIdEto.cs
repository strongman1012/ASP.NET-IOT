using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace IotWebApi.Entities
{
    public class VersionIdEto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? Category { get; set; }
        public int Ver { get; set; } = 1;

    }
}
