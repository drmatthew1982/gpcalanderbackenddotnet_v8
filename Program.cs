using Microsoft.EntityFrameworkCore;
using UserApi.Models;
using EventApi.Models;
using ClientApi.Models;
using OrganisitionApi.Models;
using MedicalRecordApi.Models;
//using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//https://medium.com/@nizzola.dev/how-to-solve-jsonexception-a-possible-object-cycle-was-detected-9a349439c3cd
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var connetionString =builder.Configuration.GetConnectionString("MySql");

builder.Services.AddMySqlDataSource(connetionString!);
builder.Services.AddDbContext<UserContext>(opt =>
    opt.UseMySql(connetionString,ServerVersion.AutoDetect(connetionString)));
builder.Services.AddDbContext<EventContext>(opt =>
    opt.UseMySql(connetionString,ServerVersion.AutoDetect(connetionString)));    
builder.Services.AddDbContext<ClientContext>(opt =>
    opt.UseMySql(connetionString,ServerVersion.AutoDetect(connetionString)));    
builder.Services.AddDbContext<OrganisationContext>(opt =>
    opt.UseMySql(connetionString,ServerVersion.AutoDetect(connetionString)));    
builder.Services.AddDbContext<MedicalRecordContext>(opt =>
    opt.UseMySql(connetionString,ServerVersion.AutoDetect(connetionString)));  
builder.Services.AddSwaggerGen();
//  https://learn.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-8.0
var  MyAllowSpecificOrigins = "AllowAll";
builder.Services.AddCors(options =>
    {
        options.AddPolicy(MyAllowSpecificOrigins,
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
