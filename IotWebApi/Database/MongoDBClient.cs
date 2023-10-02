using IotWebApi.Helpers;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace IotWebApi.Database
{
    public interface IMongoDBClient
    {
        public IMongoDatabase GetDatabase(string dbname);
        public IMongoCollection<T> GetCollection<T>();
        public IMongoCollection<T> GetCollection<T>(string collection);
        public IMongoCollection<T> GetCollection<T>(string db, string collection);
        public IMongoCollection<T> GetCollectionByDb<T>(string db);

    }
    public class MongoDBClient : IMongoDBClient
    {
        private readonly AppSettings _appSettings;
        public MongoClient dbClient = null;
        public IMongoDatabase db2 = null;

        public MongoDBClient(IOptions<AppSettings> appSettings) {
            _appSettings = appSettings.Value;
            dbClient = new MongoClient(_appSettings.DBServer);
            db2 = dbClient.GetDatabase(_appSettings.DBSchema);
        }

        public string GetDbName (string name)
        {
            if (name.ToLower().EndsWith("eto"))
                return name.Substring(0, name.Length - 3);
            return name;
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            return db2.GetCollection<T>(GetDbName(typeof(T).Name));
        }


        public IMongoCollection<T> GetCollection<T>(string collection)
        {
            return db2.GetCollection<T>(collection);
        }
        public IMongoCollection<BsonDocument> GetCollection(string collection)
        {
            return db2.GetCollection<BsonDocument>(collection);
        }
        public IMongoCollection<T> GetCollection<T>(string dbname, string collection)
        {
            if (dbname == _appSettings.DBSchema)
            {
                return GetCollection<T>(collection);
            }
            IMongoDatabase dbtemp = dbClient.GetDatabase(dbname);
            return dbtemp.GetCollection<T>(collection);
        }

        public IMongoCollection<T> GetCollectionByDb<T>(string dbname)
        {
            if (dbname == _appSettings.DBSchema)
            {
                return GetCollection<T>();
            }
            IMongoDatabase dbtemp = dbClient.GetDatabase(dbname);
            return dbtemp.GetCollection<T>(GetDbName(typeof(T).Name));
        }


        public IMongoDatabase GetDatabase(string dbname)
        {
            if (dbname == _appSettings.DBSchema)
            {
                return db2;
            }
            IMongoDatabase dbtemp = dbClient.GetDatabase(dbname);
            return dbtemp;
        }
    }
}
