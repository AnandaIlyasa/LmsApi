using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
namespace LmsApi.Model;

[Table("t_m_class")]
[Index(nameof(ClassCode), IsUnique = true, Name = "class_bk")]
public class Class : BaseModel
{
    [Column("class_code"), MaxLength(10)]
    public string ClassCode { get; set; }

    [Column("class_title"), MaxLength(50)]
    public string ClassTitle { get; set; }

    [Column("class_description")]
    public string? ClassDescription { get; set; }

    [Column("teacher_id")]
    public int TeacherId { get; set; }

    [ForeignKey(nameof(TeacherId))]
    public User Teacher { get; set; }

    [Column("class_image_id")]
    public int ClassImageId { get; set; }

    [ForeignKey(nameof(ClassImageId))]
    public LMSFile ClassImage { get; set; }

    [NotMapped]
    public List<Learning> LearningList { get; set; } // not mapped
}
