using AutoMapper;
using IotWebApi.Database;
using IotWebApi.Dto;
using IotWebApi.Entities;
using IotWebApi.Helpers;
using MongoDB.Driver;

namespace IotWebApi.Services
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetAll();
        CategoryDto? GetById(string id);
        string Create(CategoryDto u);
        bool Remove(string id);
        string Update(CategoryDto u, string id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly IMongoDBClient _client;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, IMongoDBClient client)
        {
            _client = client;
            _mapper = mapper;
        }

        public IEnumerable<CategoryDto> GetAll()
        {
            var category = _client.GetCollection<CategoryEto>().Find(_ => true).ToList();
            return _mapper.Map<IEnumerable<CategoryDto>>(category);
        }

        public CategoryDto? GetById(string id)
        {
            var category = _client.GetCollection<CategoryEto>().Find(x => x.Id == id).FirstOrDefault();
            return _mapper.Map<CategoryDto>(category);
        }

        public string Create(CategoryDto u)
        {
            var category = _client.GetCollection<CategoryEto>().Find(x => x.CategoryName == u.CategoryName).FirstOrDefault();
            if (category == null)
            {
                var categoryEto = _mapper.Map<CategoryEto>(u);
                _client.GetCollection<CategoryEto>().InsertOne(categoryEto);
                return categoryEto.Id;
            }
            return "";
        }

        public bool Remove(string id)
        {
            _client.GetCollection<CategoryEto>().DeleteOne(x => x.Id == id);
            return true;
        }


        public string Update(CategoryDto u, string id)
        {
            CategoryEto category = _client.GetCollection<CategoryEto>().Find(x => x.Id == id).FirstOrDefault();
            if (category != null)
            {
                category.CategoryName = u.CategoryName; category.CategoryDescription = u.CategoryDescription;
                category.ImageURL = u.ImageURL; category.IsActive = u.IsActive;
                category.DateModified = DateTime.UtcNow;
                _client.GetCollection<CategoryEto>().ReplaceOne(x => x.Id == id, category);
                return category.Id;
            }
            return "";
        }
    }
}
