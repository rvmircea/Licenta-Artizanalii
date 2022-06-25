using Artizanalii_Api.Data;
using Artizanalii_Api.Repositories.Beers;
using Artizanalii_Api.Repositories.ProducerAddresses;
using Artizanalii_Api.Repositories.Producers;
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

builder.Services.AddTransient<IBeerRepository, BeerRepository>();
builder.Services.AddTransient<IProducerRepository, ProducerRepository>();
builder.Services.AddTransient<IProducerAddressRepository, ProducerAddressRepository>();
builder.Services.AddTransient<IWineRepository, WineRepository>();

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