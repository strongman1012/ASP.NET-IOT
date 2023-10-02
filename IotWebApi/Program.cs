using IotWebApi.Authorization;
using IotWebApi.Configuration;
using IotWebApi.Database;
using IotWebApi.Helpers;
using IotWebApi.Services;
using IotWebApi.Setup;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();

builder.Services.AddControllers();

// configure strongly typed settings object
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// configure DI for application services
builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IDeviceModelService, DeviceModelService>();
builder.Services.AddScoped<IOrganisationService, OrganisationService>();
builder.Services.AddSingleton<IMongoDBClient, MongoDBClient>();

builder.Services.AddAutoMapper(typeof(MapperConfig));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();

// app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();
SetupBase.InstallOrUpgrade(app.Services);

app.Run();
