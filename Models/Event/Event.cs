using System.ComponentModel.DataAnnotations.Schema;
using ClientApi.Models;
using OrganisitionApi.Models;
namespace EventApi.Models;

[Table("events")]
public class Event
{
    public long Id { get; set; }

    public string? Eventcmt { get; set; }

    public long Client_id { get; set; }

    public long Org_id { get; set; }

    public long Created_user_id { get; set; }

    public long Assigned_to { get; set; }

    public DateOnly Eventdate { get; set; }

    public DateOnly? EventEndDate { get; set; }

    public string? StartTimeStr { get; set; }

    public string? EndTimeStr { get; set; }

    public DateTime? Create_time { get; set; }

    public long? Modified_user_id { get; set; }

    public DateTime? Modified_time { get; set; }

    public string? ReportStatus { get; set; }

    //https://stackoverflow.com/questions/18215376/have-model-contain-field-without-adding-it-to-the-database
    [NotMapped]
    public string? Title { get { return Eventcmt; } }

    [NotMapped]
    public string? Start => Eventdate.ToString("yyyy-MM-dd") + "T" + StartTimeStr;

    [NotMapped]
    public string? End  => Eventdate.ToString("yyyy-MM-dd")+"T"+EndTimeStr;

    public Client Client { get; set; } = null!; 
    public Organisation Organisation { get; set; } = null!; 

    public string? Firstname { get{return Client.Firstname; }}

    public string? Middlename { get{return Client.Middlename; }}

    public string? Lastname { get{return  Client.Lastname; } }

    public string? Client_id_no { get{return Client.Client_id_no; }}

    public string? Org_code { get{return Organisation.Org_code; }  }

    public string? Org_name { get{return Organisation.Org_name; }  }

}
