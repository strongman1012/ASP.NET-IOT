using AutoMapper;
using IotWebApi.Database;
using IotWebApi.Dto;
using IotWebApi.Entities;
using IotWebApi.Helpers;
using MongoDB.Driver;

namespace IotWebApi.Services
{
    public interface IDeviceModelService
    {
        IEnumerable<DeviceModelDto> GetAll();
        DeviceModelDto? GetById(string id);
        string Create(DeviceModelDto u);
        bool Remove(string id);
        string Update(DeviceModelDto u, string id);
    }

    public class DeviceModelService : IDeviceModelService
    {
        private readonly IMongoDBClient _client;
        private readonly IMapper _mapper;

        public DeviceModelService(IMapper mapper, IMongoDBClient client)
        {
            _client = client;
            _mapper = mapper;
        }

        public IEnumerable<DeviceModelDto> GetAll()
        {
            var category = _client.GetCollection<DeviceModelEto>().Find(_ => true).ToList();
            return _mapper.Map<IEnumerable<DeviceModelDto>>(category);
        }

        public DeviceModelDto? GetById(string id)
        {
            var category = _client.GetCollection<DeviceModelEto>().Find(x => x.Id == id).FirstOrDefault();
            return _mapper.Map<DeviceModelDto>(category);
        }

        public string Create(DeviceModelDto u)
        {
            var category = _client.GetCollection<DeviceModelEto>().Find(x => x.ModelNo == u.ModelNo).FirstOrDefault();
            if (category == null)
            {
                var categoryEto = _mapper.Map<DeviceModelEto>(u);
                _client.GetCollection<DeviceModelEto>().InsertOne(categoryEto);
                return categoryEto.Id;
            }
            return "";
        }

        public bool Remove(string id)
        {
            _client.GetCollection<DeviceModelEto>().DeleteOne(x => x.Id == id);
            return true;
        }


        public string Update(DeviceModelDto u, string id)
        {
            DeviceModelEto category = _client.GetCollection<DeviceModelEto>().Find(x => x.Id == id).FirstOrDefault();
            if (category != null)
            {
                category.ModelNo = u.ModelNo; category.ModelDescription = u.ModelDescription;
                category.CategoryID = u.CategoryID; category.TransmissionType = u.TransmissionType;
                category.Brand = u.Brand; category.ProductURL = u.ProductURL;
                category.DateModified = DateTime.UtcNow;
                _client.GetCollection<DeviceModelEto>().ReplaceOne(x => x.Id == id, category);
                return category.Id;
            }
            return "";
        }
    }
}
