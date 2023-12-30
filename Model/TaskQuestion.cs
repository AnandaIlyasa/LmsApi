using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LmsApi.Model;

[Table("t_m_task_question")]
public class TaskQuestion : BaseModel
{
    [Column("question_type"), MaxLength(20)]
    public string QuestionType { get; set; }

    [Column("question_content")]
    public string? QuestionContent { get; set; }

    [NotMapped]
    public List<TaskMultipleChoiceOption> OptionList { get; set; } // not mapped
}
