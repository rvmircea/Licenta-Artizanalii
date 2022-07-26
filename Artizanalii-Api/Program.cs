using Artizanalii_Api.Data;
using Artizanalii_Api.Repositories.Beers;
using Artizanalii_Api.Repositories.Categories;
using Artizanalii_Api.Repositories.ProducerAddresses;
using Artizanalii_Api.Repositories.Producers;
using Artizanalii_Api.Repositories.Products;
using Artizanalii_Api.Repositories.Wines;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<ArtizanaliiContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://vite-m0ts504hs-rvmircea.vercel.app/", "https://vite-rvmircea.vercel.app/","http://localhost:7094",
                "https://localhost:7094", "http://localhost:5173", "http://localhost:3000").AllowCredentials().AllowAnyHeader().AllowAnyMethod();
    });
});

/*builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(b => b.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
});*/

builder.Services.AddScoped<IBeerRepository, BeerRepository>();
builder.Services.AddScoped<IProducerRepository, ProducerRepository>();
builder.Services.AddScoped<IProducerAddressRepository, ProducerAddressRepository>();
builder.Services.AddScoped<IWineRepository, WineRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();


app.UseAuthorization();

app.MapControllers();

app.Run();