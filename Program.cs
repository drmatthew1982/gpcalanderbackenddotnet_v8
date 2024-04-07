using Microsoft.EntityFrameworkCore;
using UserApi.Models;
using EventApi.Models;
//using Microsoft.Extensions.Configuration;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var connetionString =builder.Configuration.GetConnectionString("MySql");

builder.Services.AddMySqlDataSource(connetionString!);
builder.Services.AddDbContext<UserContext>(opt =>
    opt.UseMySql(connetionString,ServerVersion.AutoDetect(connetionString)));
builder.Services.AddDbContext<EventContext>(opt =>
    opt.UseMySql(connetionString,ServerVersion.AutoDetect(connetionString)));    
builder.Services.AddSwaggerGen();
//  https://learn.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-8.0
var  MyAllowSpecificOrigins = "AllowAll";
builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll",
            policy =>
            {
                policy.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
    });
var app = builder.Build();
app.UseCors("AllowAll");
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
