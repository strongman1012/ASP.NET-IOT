using MongoDB.Bson;
using System.Text.Json.Serialization;
namespace IotWebApi.Dto
{
    public class OrganisationDto
    {
        public string Id { get; set; } = string.Empty;
        public string ParentOrganisationId { get; set; } = string.Empty;
        public string ParentOrganisationName { get; set; } = string.Empty;
        public string OrganisationName { get; set; }
        public string OrganisationAddress { get; set; }
        public string OrganisationContactId { get; set; }
        public string OrganisationContactName { get; set; }
        public int BillingId { get; set; }
        public string Slug { get; set; }
        public string Active { get; set; }
  
    }
}
