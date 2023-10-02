using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
namespace IotWebApi.Entities
{
    public class CategoryEto : BaseEto
    {
        public string? CategoryName { get; set; } = string.Empty;
        public string? CategoryDescription { get; set; }
        public string? ImageURL { get; set; }
        public string? IsActive { get; set; }
    }
}
