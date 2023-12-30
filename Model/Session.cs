using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LmsApi.Model;

[Table("t_m_session")]
public class Session : BaseModel
{
    [Column("session_name"), MaxLength(50)]
    public string SessionName { get; set; }

    [Column("session_description")]
    public string? SessionDescription { get; set; }

    [Column("start_time")]
    public TimeOnly StartTime { get; set; }

    [Column("end_time")]
    public TimeOnly EndTime { get; set; }

    [Column("learning_id")]
    public int LearningId { get; set; }

    [ForeignKey(nameof(LearningId))]
    public Learning Learning { get; set; }

    [NotMapped]
    public Forum Forum { get; set; } // not mapped

    [NotMapped]
    public List<SessionMaterial> MaterialList { get; set; } // not mapped

    [NotMapped]
    public List<LMSTask> TaskList { get; set; } // not mapped
}
