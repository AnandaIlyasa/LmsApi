using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
namespace LmsApi.Model;

[Table("t_m_user")]
[Index(nameof(Email), IsUnique = true, Name = "email_bk")]
public class User : BaseModel
{
    [Column("full_name"), MaxLength(30)]
    public string FullName { get; set; }

    [Column("email"), MaxLength(30)]
    public string Email { get; set; }

    [Column("pass")]
    public string Pass { get; set; }

    [Column("photo_id")]
    public int? PhotoId { get; set; }

    [ForeignKey(nameof(PhotoId))]
    public LMSFile? Photo { get; set; }

    [Column("role_id")]
    public int RoleId { get; set; }

    [ForeignKey(nameof(RoleId))]
    public Role Role { get; set; }
}
