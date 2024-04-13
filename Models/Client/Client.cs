using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using EventApi.Models;
using Microsoft.AspNetCore.SignalR.Protocol;
namespace ClientApi.Models;

[Table("clients")]
public class Client
{
    public long Id { get; set; }

    public string? Firstname { get; set; }

    public string? Middlename { get ; set; }

    public string? Lastname { get; set; }

    public long Created_user_id { get; set; }

    public long Modified_user_id { get; set; }

    public DateTime? Created_time { get; set; }

    public DateTime? Modified_time { get; set; }

    public DateTime? Birthday { get; set; }
    
    public string? Gender { get; set; }
    
    public string? Client_id_no { get; set; }
    //https://stackoverflow.com/questions/60197270/jsonexception-a-possible-object-cycle-was-detected-which-is-not-supported-this
    [JsonIgnore]
    public ICollection<Event> Events { get; } = new List<Event>(); 

}
