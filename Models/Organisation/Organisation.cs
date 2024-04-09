using System.ComponentModel.DataAnnotations.Schema;
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
}
