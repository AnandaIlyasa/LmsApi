using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LmsApi.Model;

[Table("t_m_task_multiple_choice_option")]
[Index(nameof(OptionChar), nameof(QuestionId), IsUnique = true, Name = "multiple_choice_option_ck")]
public class TaskMultipleChoiceOption : BaseModel
{
    [Column("option_char", TypeName = "char(1)")]
    public string OptionChar { get; set; }

    [Column("option_text"), MaxLength(255)]
    public string OptionText { get; set; }

    [Column("is_correct")]
    public bool IsCorrect { get; set; }

    [Column("question_id")]
    public int QuestionId { get; set; }

    [ForeignKey(nameof(QuestionId))]
    public TaskQuestion Question { get; set; }
}
