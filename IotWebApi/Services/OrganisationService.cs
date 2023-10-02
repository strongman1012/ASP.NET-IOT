using AutoMapper;
using IotWebApi.Database;
using IotWebApi.Dto;
using IotWebApi.Entities;
using IotWebApi.Helpers;
using MongoDB.Driver;

namespace IotWebApi.Services
{
    public interface IOrganisationService
    {
        IEnumerable<OrganisationDto> GetAll();
        IEnumerable<OrganisationDto> GetAllParent();
        OrganisationDto? GetById(string id);
        string Create(OrganisationDto u);
        bool Remove(string id);
        string Update(OrganisationDto u, string id);
    }
    public class OrganisationService : IOrganisationService
    {
        private readonly IMongoDBClient _client;
        private readonly IMapper _mapper;

        public OrganisationService(IMapper mapper, IMongoDBClient client)
        {
            _client = client;
            _mapper = mapper;
        }

        public IEnumerable<OrganisationDto> GetAll()
        {
            var organisation = _client.GetCollection<OrganisationEto>().Find(_ => true).ToList();
            return _mapper.Map<IEnumerable<OrganisationDto>>(organisation);
        }
        public IEnumerable<OrganisationDto> GetAllParent()
        {
            var organisation = _client.GetCollection<OrganisationEto>().Find(x => x.ParentOrganisationId == "top").ToList();
            return _mapper.Map<IEnumerable<OrganisationDto>>(organisation);
        }

        public OrganisationDto? GetById(string id)
        {
            var organisation = _client.GetCollection<OrganisationEto>().Find(x => x.Id == id).FirstOrDefault();
            return _mapper.Map<OrganisationDto>(organisation);
        }

        public string Create(OrganisationDto u)
        {
            var organisation = _client.GetCollection<OrganisationEto>().Find(x => x.OrganisationContactId == u.OrganisationContactId).FirstOrDefault();
            if (organisation == null)
            {
                var organisationEto = _mapper.Map<OrganisationEto>(u);
                _client.GetCollection<OrganisationEto>().InsertOne(organisationEto);
                return organisationEto.Id;
            }
            return "";
        }

        public bool Remove(string id)
        {
            _client.GetCollection<OrganisationEto>().DeleteOne(x => x.Id == id);
            return true;
        }


        public string Update(OrganisationDto u, string id)
        {
            OrganisationEto organisation = _client.GetCollection<OrganisationEto>().Find(x => x.Id == id).FirstOrDefault();
            if (organisation != null)
            {
                organisation.ParentOrganisationId = u.ParentOrganisationId; organisation.ParentOrganisationName = u.ParentOrganisationName;
                organisation.OrganisationName = u.OrganisationName; organisation.OrganisationAddress = u.OrganisationAddress;
                organisation.OrganisationContactId = u.OrganisationContactId; organisation.OrganisationContactName = u.OrganisationContactName;
                organisation.BillingId = u.BillingId; organisation.Slug = u.Slug; organisation.Active = u.Active;

                organisation.DateModified = DateTime.UtcNow;
                _client.GetCollection<OrganisationEto>().ReplaceOne(x => x.Id == id, organisation);
                return organisation.Id;
            }
            return "";
        }
    }
}
