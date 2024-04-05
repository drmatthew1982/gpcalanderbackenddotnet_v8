using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using UserApi.Models;
//using Microsoft.Extensions.Configuration;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<TodoContext>(opt =>
    opt.UseInMemoryDatabase("TodoList"));
var connetionString =builder.Configuration.GetConnectionString("MySql");

builder.Services.AddMySqlDataSource(connetionString!);
builder.Services.AddDbContext<UserContext>(opt =>
    opt.UseMySql(connetionString,ServerVersion.AutoDetect(connetionString)));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//var  https://learn.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-8.0
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
