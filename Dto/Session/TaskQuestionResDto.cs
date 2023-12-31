namespace LmsApi.Dto.Session;

public class TaskQuestionResDto
{
    public int Id { get; set; }
    public string QuestionContent { get; set; }
    public string QuestionType { get; set; }
    public List<MultipleChoiceOptionsResDto>? OptionList { get; set; }
}
