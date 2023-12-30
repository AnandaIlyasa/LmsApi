using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LmsApi.Model;

[Table("t_m_task")]
public class LMSTask : BaseModel
{
    [Column("task_name"), MaxLength(30)]
    public string TaskName { get; init; }

    [Column("task_description")]
    public string? TaskDescription { get; init; }

    [Column("duration")]
    public int Duration { get; init; }

    [Column("session_id")]
    public int SessionId { get; set; }

    [ForeignKey(nameof(SessionId))]
    public Session Session { get; init; }

    [NotMapped]
    public List<TaskFile> TaskFileList { get; set; } // not mapped

    [NotMapped]
    public List<TaskQuestion> TaskQuestionList { get; set; } // not mapped
}
