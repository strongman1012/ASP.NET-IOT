using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
namespace IotWebApi.Entities
{
    public class OrganisationEto : BaseEto
    {
        public string ParentOrganisationId { get; set; } = string.Empty;
        public string ParentOrganisationName { get; set; }
        public string OrganisationName { get; set; }
        public string OrganisationAddress { get; set; }
        public string OrganisationContactId { get; set; }
        public string OrganisationContactName { get; set; }
        public int BillingId { get; set; }
        public string Slug { get; set; }
        public string Active { get; set; }
    }
}
