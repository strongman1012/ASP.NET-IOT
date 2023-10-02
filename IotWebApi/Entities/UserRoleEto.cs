using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace IotWebApi.Entities
{
    public class UserRoleEto : BaseEto
    {
        public string? UserId { get; set; }
        public string? RoleId { get; set; }
    }
}
