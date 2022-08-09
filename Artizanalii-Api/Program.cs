using System.Configuration;
using System.Security.Claims;
using Artizanalii_Api.Data;
using Artizanalii_Api.Entities.BasketItems;
using Artizanalii_Api.Helpers.Auth;
using Artizanalii_Api.Repositories.Baskets;
using Artizanalii_Api.Repositories.Beers;
using Artizanalii_Api.Repositories.Categories;
using Artizanalii_Api.Repositories.Orders;
using Artizanalii_Api.Repositories.ProducerAddresses;
using Artizanalii_Api.Repositories.Producers;
using Artizanalii_Api.Repositories.Products;
using Artizanalii_Api.Repositories.Wines;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Stripe;

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

//Stripe
StripeConfiguration.ApiKey =
    "sk_test_51KcmBYD7EKw07oOm3YxPjnCcs6S7bbWyXrGQk4b5GwQabV8dn8dBeJzpqZlfZGJgYxKMNuzqVUFNQ2Gd4L9iiJD700P5iQ1dms";




//Auth0
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}/";
    options.Audience = builder.Configuration["Auth0:Audience"];
    options.TokenValidationParameters = new TokenValidationParameters {NameClaimType = ClaimTypes.NameIdentifier};
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("buy:products", policy =>
        policy.RequireClaim("permissions", "buy:products"));
});

builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://play.google.com/log?format=json&hasfast=true", "https://vite-m0ts504hs-rvmircea.vercel.app/", "https://vite-rvmircea.vercel.app/","http://localhost:7094",
                "https://localhost:7094", "http://localhost:5173", "http://localhost:3000").AllowCredentials().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddScoped<IBeerRepository, BeerRepository>();
builder.Services.AddScoped<IProducerRepository, ProducerRepository>();
builder.Services.AddScoped<IProducerAddressRepository, ProducerAddressRepository>();
builder.Services.AddScoped<IWineRepository, WineRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();