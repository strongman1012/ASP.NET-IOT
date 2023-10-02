using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
namespace IotWebApi.Entities
{
    public class DeviceModelEto : BaseEto
    {
        public string ModelNo { get; set; } = string.Empty;
        public string? ModelDescription { get; set; }
        public string? CategoryID { get; set; }
        public string? TransmissionType { get; set; }
        public string? Brand { get; set; }
        public string? ProductURL { get; set; }
    }
}
