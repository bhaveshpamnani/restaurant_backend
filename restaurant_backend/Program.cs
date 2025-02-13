using System.Reflection;
using System.Text;
using CloudinaryDotNet;
using DefaultNamespace;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using restaurant_backend.Data;
using restaurant_backend.Filters;
using restaurant_backend.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Restaurent", Version = "v1" });
    c.OperationFilter<SwaggerFileUploadOperationFilter>(); // Register the filter for file uploads
});

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<FeedbackRepository>();
builder.Services.AddScoped<ReservationRepository>();
builder.Services.AddScoped<MenuRepository>();
builder.Services.AddScoped<InventoryRepository>();
builder.Services.AddScoped<FeedbackCategoryRepository>();
builder.Services.AddScoped<DinningTableRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<CustomerOrderRepository>();
builder.Services.AddScoped<CartRepository>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddControllers().AddFluentValidation(f => f.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
// Add services for controllers
builder.Services.AddControllers();

// Register Cloudinary configuration and service
builder.Services.AddScoped<CloudinaryService>();
var cloudinarySettings = builder.Configuration.GetSection("Cloudinary");
var cloudinaryAccount = new Account(
    cloudinarySettings["CloudName"],
    cloudinarySettings["ApiKey"],
    cloudinarySettings["ApiSecret"]
);
var cloudinary = new Cloudinary(cloudinaryAccount);
builder.Services.AddSingleton(cloudinary);

// Add Swagger for API documentation



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

// Map controllers
app.MapControllers();

app.Run();

