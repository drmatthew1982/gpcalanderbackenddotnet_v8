using System.ComponentModel.DataAnnotations.Schema;
namespace MedicalRecordApi.Models;

[Table("medical_records")]
public class MedicalRecord
{
    public long Id { get; set; }

    public long Eventid { get; set; }

    public string? Summary { get; set; }
    
    public string? Positions { get; set; }
    
    public long Created_user_id { get; set; }

    public long Modified_user_id { get; set; }

    public DateTime? Created_time { get; set; }

    public DateTime? Modified_time { get; set; }
}
