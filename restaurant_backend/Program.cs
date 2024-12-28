using System.Reflection;
using FluentValidation.AspNetCore;
using restaurant_backend.Data;
using restaurant_backend.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<FeedbackRepository>();
builder.Services.AddScoped<ReservationRepository>();
builder.Services.AddControllers().AddFluentValidation(f => f.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
// Add services for controllers
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthorization();

// Map controllers
app.MapControllers();

app.Run();
