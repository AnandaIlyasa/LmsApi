using System.ComponentModel.DataAnnotations.Schema;

namespace LmsApi.Model;

[Table("t_r_submission_detail_question")]
public class SubmissionDetailQuestion : BaseModel
{
    [Column("essay_answer_content")]
    public string? EssayAnswerContent { get; set; }

    [Column("submission_id")]
    public int SubmissionId { get; set; }

    [ForeignKey(nameof(SubmissionId))]
    public Submission Submission { get; set; }

    [Column("question_id")]
    public int QuestionId { get; set; }

    [ForeignKey(nameof(QuestionId))]
    public TaskQuestion Question { get; set; }

    [Column("choice_option_id")]
    public int? ChoiceOptionId { get; set; }

    [ForeignKey(nameof(ChoiceOptionId))]
    public TaskMultipleChoiceOption? ChoiceOption { get; set; }
}
