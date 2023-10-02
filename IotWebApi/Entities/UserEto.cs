using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace IotWebApi.Entities
{
    public class UserEto : BaseEto
    {
        public string? Username { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? Active { get; set; }
        public string? RoleId { get; set; }
        public DateTime? LastLogin { get; set; }

        [JsonIgnore]
        public string? Password { get; set; }

        public string GetName()
        {
            if (string.IsNullOrEmpty(FirstName))
            {
                return LastName;
            }
            if (string.IsNullOrEmpty(LastName))
            {
                return FirstName;
            }
            return FirstName + " " + LastName;
        }
    }
}
