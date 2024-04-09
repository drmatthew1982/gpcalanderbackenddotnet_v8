using System.ComponentModel.DataAnnotations.Schema;
namespace MedicalRecordApi.Models;

[Table("medical_record")]
public class MedicalRecord
{
    public long Id { get; set; }

    public string? event_id { get; set; }

    public string? summary { get; set; }
    
    public string? positions { get; set; }
    
    public long Created_user_id { get; set; }

    public long Modified_user_id { get; set; }

    public DateTime? Created_time { get; set; }

    public DateTime? Modified_time { get; set; }
}
