using MongoDB.Bson;
using System.Text.Json.Serialization;
namespace IotWebApi.Dto
{
    public class DeviceModelDto
    {
        public string Id { get; set; } = string.Empty;
        public string ModelNo { get; set; } = string.Empty;
        public string? ModelDescription { get; set; }
        public string? CategoryID { get; set; }
        public string? TransmissionType { get; set; }
        public string? Brand { get; set; }
        public string? ProductURL { get; set; }
    }
}
