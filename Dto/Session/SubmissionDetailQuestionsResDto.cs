using LmsApi.Dto.File;

namespace LmsApi.Dto.Session;

public class SubmissionDetailQuestionsResDto
{
    public int QuestionId { get; set; }
    public string? EssayAnswerContent { get; set; }
    public int? ChoiceOptionId { get; set; }
}
