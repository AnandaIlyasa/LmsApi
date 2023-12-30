using System.ComponentModel.DataAnnotations.Schema;

namespace LmsApi.Model;

[Table("t_r_task_detail")]
public class TaskDetail : BaseModel
{
    [Column("task_id")]
    public int TaskId { get; set; }

    [ForeignKey(nameof(TaskId))]
    public LMSTask Task { get; set; }

    [Column("task_file_id")]
    public int? TaskFileId { get; set; }

    [ForeignKey(nameof(TaskFileId))]
    public TaskFile? TaskFile { get; set; }

    [Column("task_question_id")]
    public int? TaskQuestionId { get; set; }

    [ForeignKey(nameof(TaskQuestionId))]
    public TaskQuestion? TaskQuestion { get; set; }
}
