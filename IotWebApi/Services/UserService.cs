using AutoMapper;
using IotWebApi.Authorization;
using IotWebApi.Database;
using IotWebApi.Dto;
using IotWebApi.Entities;
using IotWebApi.Helpers;
using MongoDB.Driver;
using Syncfusion.EJ2.Base;

namespace IotWebApi.Services
{
    public interface IUserService
    {
        AuthenticateResponseDto? Authenticate(AuthenticateRequestDto model);
        IEnumerable<UserDto> GetAll();
        IEnumerable<AuditLogDto> GetAllLog();
        CollectionResult<UserDto> GetAllWithDM(DataManagerRequest dm);
        UserDto? GetById(string id);
        UserDto? GetByEmail(string email);
        UserDto? GetByUsername(string username);
        string Create(UserDto u, string password);
        string CreateLog(AuditLogDto u);
        bool Remove(string id);
        bool RemoveByUsername(string username);
        string Update(UserDto u, string id);
    }

    public class UserService : IUserService
    {
        private readonly IJwtUtils _jwtUtils;
        private readonly IMongoDBClient _client;
        private readonly IMapper _mapper;

        public UserService(IMapper mapper, IMongoDBClient client, IJwtUtils jwtUtils)
        {
            _jwtUtils = jwtUtils;
            _client = client;
            _mapper = mapper;
        }

        public AuthenticateResponseDto? Authenticate(AuthenticateRequestDto model)
        {
            var userEto = _client.GetCollection<UserEto>().Find(x => x.Email == model.Email && x.Password == model.Password).FirstOrDefault();
            var user = _mapper.Map<UserDto>(userEto);

            // return null if user not found
            if (user == null) return null;

            var roleEto = _client.GetCollection<RoleEto>().Find(x => x.Id == userEto.RoleId).FirstOrDefault();
            var urole = roleEto.RoleName;
            // authentication successful so generate jwt token
            var token = _jwtUtils.GenerateJwtToken(userEto);

            userEto.LastLogin = DateTime.UtcNow;
            _client.GetCollection<UserEto>().ReplaceOne(x => x.Id == userEto.Id, userEto);

            return new AuthenticateResponseDto(user, urole, token);
        }

        public IEnumerable<UserDto> GetAll()
        {
            var user = _client.GetCollection<UserEto>().Find(_ => true).ToList();
            return _mapper.Map<IEnumerable<UserDto>>(user);
        }
        public IEnumerable<AuditLogDto> GetAllLog()
        {
            var log = _client.GetCollection<AuditLogEto>().Find(_ => true).ToList();
            return _mapper.Map<IEnumerable<AuditLogDto>>(log);
        }

        public CollectionResult<UserDto> GetAllWithDM(DataManagerRequest dm)
        {
            // var user = _client.GetCollection<UserEto>().Find(_ => true).ToList();
            // return _mapper.Map<IEnumerable<UserDto>>(user);

            var query = (IQueryable<UserEto>)_client.GetCollection<UserEto>().AsQueryable();
            QueryableOperation operation = new QueryableOperation();

            if (dm.Where != null)
                query = operation.PerformFiltering(query, dm.Where, dm.Where[0].Condition);
            if (dm.Search != null)
                query = operation.PerformSearching(query, dm.Search);
            int count = query.Count();
            if (dm.Sorted != null)
                query = operation.PerformSorting(query, dm.Sorted);
            if (dm.Select != null)
                query = (IQueryable<UserEto>)operation.PerformSelect(query, dm.Select);
            if (dm.Skip != 0)
                query = operation.PerformSkip(query, dm.Skip);
            if (dm.Take != 0)
                query = operation.PerformTake(query, dm.Take);
            var users = _mapper.Map<IEnumerable<UserDto>>(query.ToList());
            return new CollectionResult<UserDto>() { Result = users, Count = count };

        }


        public UserDto? GetById(string id)
        {
            var user = _client.GetCollection<UserEto>().Find(x => x.Id == id).FirstOrDefault();
            return _mapper.Map<UserDto>(user);
        }

        public UserDto? GetByUsername(string username)
        {
            var user = _client.GetCollection<UserEto>().Find(x => x.Username == username).FirstOrDefault();
            return _mapper.Map<UserDto>(user);
        }
        public UserDto? GetByEmail(string email)
        {
            var user = _client.GetCollection<UserEto>().Find(x => x.Email == email).FirstOrDefault();
            return _mapper.Map<UserDto>(user);
        }


        public string Create(UserDto u, string password = null)
        {
            var user = _client.GetCollection<UserEto>().Find(x => x.Email == u.Email).FirstOrDefault();
            if (user == null)
            {
                var userEto = _mapper.Map<UserEto>(u);
                if (string.IsNullOrEmpty(password))
                {
                    password = "default";
                }
                userEto.Password = password;
                _client.GetCollection<UserEto>().InsertOne(userEto);
                return userEto.Id;
            }
            return "";
        }
        public string CreateLog(AuditLogDto u)
        {
            var auditEto = _mapper.Map<AuditLogEto>(u);
            auditEto.Time = DateTime.Now;
            _client.GetCollection<AuditLogEto>().InsertOne(auditEto);
            return auditEto.Id;
        }

        public bool Remove(string id)
        {
            _client.GetCollection<UserEto>().DeleteOne(x => x.Id == id);
            return true;
        }

        public bool RemoveByUsername(string username)
        {
            var user = _client.GetCollection<UserEto>().Find(x => x.Username == username).FirstOrDefault();
            if (user != null)
            {
                return Remove(user.Id);
            }
            return false;
        }

        public string Update(UserDto u, string id)
        {
            UserEto user = _client.GetCollection<UserEto>().Find(x => x.Id == id).FirstOrDefault();
            if (user != null)
            {
                user.FirstName = u.FirstName; user.LastName = u.LastName;
                user.Email = u.Email; user.Address = u.Address;
                user.City = u.City; user.PostalCode = u.PostalCode;
                user.Active = u.Active; user.RoleId = u.RoleId;
                user.Username = u.Username; user.DateModified = DateTime.UtcNow;
                _client.GetCollection<UserEto>().ReplaceOne(x => x.Id == id, user);
                return user.Id;
            }
            return "";

        }
    }
}
