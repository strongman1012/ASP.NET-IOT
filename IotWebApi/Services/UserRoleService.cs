using IotWebApi.Authorization;
using IotWebApi.Database;
using IotWebApi.Dto;

namespace IotWebApi.Services
{
    public interface IUserRoleService
    {
        IEnumerable<UserRoleDto> GetAll();
        UserRoleDto? GetById(string id);

        UserRoleDto? GetByUsername(string username);
        bool Create(UserRoleDto u);
        bool Remove(string id);
        bool RemoveByUsername(string username);
        bool Update(UserRoleDto u);
    }

    public class UserRoleService : IUserRoleService
    {
        private readonly IJwtUtils _jwtUtils;
        private readonly IMongoDBClient _client;

        public UserRoleService(IMongoDBClient client)
        {
            _client = client;
        }

        public IEnumerable<UserRoleDto> GetAll()
        {
            return null;
        }

        public UserRoleDto? GetById(string id)
        {
            return null;
        }

        public UserRoleDto? GetByUsername(string username)
        {
            return null;
        }


        public bool Create(UserRoleDto u)
        {
            return false;
        }

        public bool Remove(string id)
        {
            return false;
        }

        public bool RemoveByUsername(string username)
        {
            return false;
        }

        public bool Update(UserRoleDto u)
        {
            return false;
        }
    }
}
