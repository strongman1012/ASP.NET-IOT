using AutoMapper;
using IotWebApi.Database;
using IotWebApi.Dto;
using IotWebApi.Entities;
using IotWebApi.Helpers;
using MongoDB.Driver;

namespace IotWebApi.Services
{
    public interface IRoleService
    {
        IEnumerable<RoleDto> GetAll();
        RoleDto? GetById(string id);
        string Create(RoleDto u);
        bool Remove(string id);
        string Update(RoleDto u, string id);
    }

    public class RoleService : IRoleService
    {
        private readonly IMongoDBClient _client;
        private readonly IMapper _mapper;

        public RoleService(IMapper mapper, IMongoDBClient client)
        {
            _client = client;
            _mapper = mapper;
        }

        public IEnumerable<RoleDto> GetAll()
        {
            var role = _client.GetCollection<RoleEto>().Find(_ => true).ToList();
            return _mapper.Map<IEnumerable<RoleDto>>(role);
        }

        public RoleDto? GetById(string id)
        {
            var role = _client.GetCollection<RoleEto>().Find(x => x.Id == id).FirstOrDefault();
            return _mapper.Map<RoleDto>(role);
        }

        public string Create(RoleDto u)
        {
            var role = _client.GetCollection<RoleEto>().Find(x => x.RoleName == u.RoleName).FirstOrDefault();
            if (role == null)
            {
                var roleEto = _mapper.Map<RoleEto>(u);
                _client.GetCollection<RoleEto>().InsertOne(roleEto);
                return roleEto.Id;
            }
            return "";
        }

        public bool Remove(string id)
        {
            _client.GetCollection<RoleEto>().DeleteOne(x => x.Id == id);
            return true;
        }


        public string Update(RoleDto u, string id)
        {
            RoleEto role = _client.GetCollection<RoleEto>().Find(x => x.Id == id).FirstOrDefault();
            if (role != null)
            {
                role.RoleName = u.RoleName; role.Context = u.Context;
                role.DateModified = DateTime.UtcNow;
                _client.GetCollection<RoleEto>().ReplaceOne(x => x.Id == id, role);
                return role.Id;
            }
            return "";
        }
    }
}
