using System.Text;
using Core.Interfaces;
using Core.Services;
using Infastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var services = builder.Services;

services.AddControllers();

// Swagger chuẩn
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API",
        Version = "v1"
    });
});

services.AddCors(option => option.AddPolicy(name: "Origin", builder =>
{
    builder.AllowAnyHeader();
    builder.AllowAnyMethod();
    builder.WithOrigins("http://localhost:8080", "http://localhost:8081");
}));

// DI Services
services.AddScoped<IBaseRepository, BaseRepository>();
services.AddScoped<IBaseService, BaseService>();
services.AddScoped<ICustomerRepository, CustomerRepository>();
services.AddScoped<ICustomerService, CustomerService>();
services.AddScoped<IProductService, ProductService>();
services.AddScoped<IProductRepository, ProductRepository>();
services.AddScoped<ICategoryService, CategoryService>();
services.AddScoped<ICategoryRepository, CategoryRepository>();
services.AddScoped<IStoreService, ProviderService>();
services.AddScoped<IStoreRepository, ProviderRepository>();
services.AddScoped<INewsService, NewsService>();
services.AddScoped<INewsRepository, NewsRepository>();
services.AddScoped<IUserService, UserService>();
services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<ICartRepository, CartRepository>();
services.AddScoped<ICartService, CartService>();
services.AddScoped<IOrderRepository, OrderRepository>();
services.AddScoped<IOrderService, OrderService>();
services.AddScoped<ILoginService, LoginService>();

// JWT Authentication
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["Jwt:Audience"],
        ValidIssuer = configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
    };
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("Origin");

app.UseAuthentication();

app.UseAuthorization();

// Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
