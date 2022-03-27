using Microsoft.EntityFrameworkCore;
using Study.Core.Repositories;
using Study.Core.UnitOfWorks;
using Study.Repository;
using Study.Repository.Repositories;
using Study.Repository.UnitOfWorks;
using System.Reflection;
using Study.Core.Services;
using Pomelo.EntityFrameworkCore.MySql;
using Study.Service.Mapping;
using Study.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseMySql(builder.Configuration.GetConnectionString("MySqlConnection"),ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySqlConnection")), options =>
    {
        options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
    

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
