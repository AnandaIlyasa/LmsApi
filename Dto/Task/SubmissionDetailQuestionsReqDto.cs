using System.ComponentModel.DataAnnotations;

namespace LmsApi.Dto.Task;

public class SubmissionDetailQuestionsReqDto
{
    [Required(ErrorMessage = "questionId is required")]
    public int QuestionId { get; set; }
    public string? EssayAnswerContent { get; set; }
    public int? ChoiceOptionId { get; set; }
}
