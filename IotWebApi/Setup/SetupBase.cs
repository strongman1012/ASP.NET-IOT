using IotWebApi.Database;
using IotWebApi.Helpers;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using IotWebApi.Entities;

namespace IotWebApi.Setup
{
    public class SetupBase
    {
        public SetupBase(IServiceProvider provider) {
        }

        public static async void InstallOrUpgrade (IServiceProvider provider)
        {
            using (var ss = provider.CreateScope())
            {
                IMongoDBClient c = ss.ServiceProvider.GetService<IMongoDBClient>();
                RunUpgrade(c);
            }
        }

        public static async void RunUpgrade (IMongoDBClient c)
        {
            IMongoCollection<VersionIdEto> coll = c.GetCollection<VersionIdEto>();
            VersionIdEto ver = await coll.Find(x => x.Category == "Base").FirstOrDefaultAsync();
            if (ver == null || ver.Ver < 1)
            {
                IMongoCollection<RoleEto> rolls = c.GetCollection<RoleEto>();

                RoleEto r = new RoleEto { RoleName = "Admin", Context = 1 };
                rolls.InsertOne(r);
                string adminid = r.Id;

                r = new RoleEto { RoleName = "CatalogAdmin", Context = 1 };
                rolls.InsertOne(r);

                r = new RoleEto { RoleName = "DeviceAdmin", Context = 1 };
                rolls.InsertOne(r);

                r = new RoleEto { RoleName = "OrganisationAdmin", Context = 2 };
                rolls.InsertOne(r);

                r = new RoleEto { RoleName = "OrganisationUser", Context = 2 };
                rolls.InsertOne(r);

                IMongoCollection<UserEto> users = c.GetCollection<UserEto>();
                UserEto u = new UserEto { Username = "admin", Email = "rakeshg@astainnosys.com", FirstName = "Rakesh", LastName = "Gupta", Password = "password" };
                users.InsertOne(u);

                coll.InsertOne(new VersionIdEto { Category = "Base", Ver = 1 });
            }

        }

    }
}
