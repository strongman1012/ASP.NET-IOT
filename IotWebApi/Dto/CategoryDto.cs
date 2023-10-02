using MongoDB.Bson;
using System.Text.Json.Serialization;
namespace IotWebApi.Dto
{
    public class CategoryDto
    {
        public string Id { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string? CategoryDescription { get; set; }
        public string? ImageURL { get; set; }
        public string? IsActive { get; set; }
    }
}
