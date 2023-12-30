using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
namespace LmsApi.Model;

[Table("t_r_student_class")]
[Index(nameof(ClassId), nameof(StudentId), IsUnique = true, Name = "student_class_ck")]
public class StudentClass : BaseModel
{
    [Column("class_id")]
    public int ClassId { get; set; }

    [ForeignKey(nameof(ClassId))]
    public Class Class { get; set; }

    [Column("student_id")]
    public int StudentId { get; set; }

    [ForeignKey(nameof(StudentId))]
    public User Student { get; set; }
}
