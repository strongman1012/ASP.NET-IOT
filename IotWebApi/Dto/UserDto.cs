using System.Text.Json.Serialization;

namespace IotWebApi.Dto
{
    public class UserDto
    {
        public string Id { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? Active { get; set; }
        public string? RoleId { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
