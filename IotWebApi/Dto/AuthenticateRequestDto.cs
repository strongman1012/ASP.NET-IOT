using System.ComponentModel.DataAnnotations;

namespace IotWebApi.Dto
{
    public class AuthenticateRequestDto
    {
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
