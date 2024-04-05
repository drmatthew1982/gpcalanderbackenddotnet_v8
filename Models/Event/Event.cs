using System.ComponentModel.DataAnnotations.Schema;
namespace EventApi.Models;

[Table("events")]
public class Event
{
    public long Id { get; set; }

    public string? eventcmt { get; set; }

    public string? client_id { get; set; }

    public string? org_id { get; set; }

    public string? createby { get; set; }

    public string? assigned_to { get; set; }

    public DateOnly? eventdate { get; set; }

    public DateOnly? eventEndDate { get; set; }

    public string? startTimeStr { get; set; }

    public string? endTimeStr { get; set; }

    public string? created_user_id { get; set; }

    public DateTime? created_time { get; set; }

    public string? modified_user_id { get; set; }

    public DateTime? modified_time { get; set; }

    public string? reportStatus { get; set; }

    public string? firstname { get; set; }

    public string? middlename { get; set; }

    public string? lastname { get; set; }

    public string? client_id_no { get; set; }

    public string? org_code { get; set; }

    public string? org_name { get; set; }

}
