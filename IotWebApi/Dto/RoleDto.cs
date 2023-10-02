using MongoDB.Bson;
using System.Text.Json.Serialization;
namespace IotWebApi.Dto
{
    public class RoleDto
    {
        public string Id { get; set; } = string.Empty;
        public string? RoleName { get; set; } = string.Empty;
        public int? Context { get; set; }
        public string? Description { get; set; }
    }
}
