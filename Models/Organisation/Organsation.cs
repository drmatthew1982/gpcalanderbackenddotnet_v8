using System.ComponentModel.DataAnnotations.Schema;
namespace OrgnasitionApi.Models;

[Table("organsations")]
public class Organsation
{
    public long Id { get; set; }

    public string? Org_name { get; set; }

    public string? Org_Code { get; set; }
    
    public long Created_user_id { get; set; }

    public long Modefied_user_id { get; set; }

    public DateTime? Created_time { get; set; }

    public DateTime? Modefied_time { get; set; }
}
