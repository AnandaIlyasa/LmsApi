using LmsApi.Dto.Session;
using System.ComponentModel.DataAnnotations;

namespace LmsApi.Dto.Task;

public class TaskQuestionsDto
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "questionContent is required")]
    public string QuestionContent { get; set; }

    [Required(ErrorMessage = "questionType is required")]
    public string QuestionType { get; set; }

    public List<MultipleChoiceOptionsDto>? OptionList { get; set; }
}
