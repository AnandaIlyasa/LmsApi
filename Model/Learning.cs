using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LmsApi.Model;

[Table("t_m_learning")]
public class Learning : BaseModel
{
    [Column("learning_name"), MaxLength(30)]
    public string LearningName { get; set; }

    [Column("learning_description")]
    public string? LearningDescription { get; set; }

    [Column("learning_date")]
    public DateOnly LearningDate { get; set; }

    [Column("class_id")]
    public int ClassId { get; set; }

    [ForeignKey(nameof(ClassId))]
    public Class Class { get; set; }

    [NotMapped]
    public List<Session> SessionList { get; set; } // not mapped
}
