using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using EventApi.Models;
namespace OrganisitionApi.Models;

[Table("organisations")]
public class Organisation
{
    public long Id { get; set; }

    public string? Org_name { get; set; }

    public string? Org_code { get; set; }
    
    public long Created_user_id { get; set; }

    public long Modified_user_id { get; set; }

    public DateTime? Created_time { get; set; }

    public DateTime? Modified_time { get; set; }
    
    //https://stackoverflow.com/questions/60197270/jsonexception-a-possible-object-cycle-was-detected-which-is-not-supported-this
    [JsonIgnore]
    public ICollection<Event> Events { get; } = new List<Event>(); 
}
