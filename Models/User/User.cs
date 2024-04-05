using System.ComponentModel.DataAnnotations.Schema;
namespace UserApi.Models;

[Table("users")]
public class User
{
    public long Id { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Firstname { get; set; }
    public string? Middlename { get; set; }
    public string? Seckey { get; set; }
}
