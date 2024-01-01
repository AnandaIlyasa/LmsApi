using System.ComponentModel.DataAnnotations;

namespace LmsApi.Dto.Class;

public class LearningInsertReqDto
{
    [Required(ErrorMessage = "learningName is required"), MaxLength(30)]
    public string LearningName { get; set; }

    [Required(ErrorMessage = "learningDescription is required")]
    public string LearningDescription { get; set; }

    [Required(ErrorMessage = "learningDate is required")]
    public string LearningDate { get; set; }
}
