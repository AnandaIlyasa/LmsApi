using System.ComponentModel.DataAnnotations;

namespace LmsApi.Dto.Session;

public class MultipleChoiceOptionsDto
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "optionChar is required"), MaxLength(1)]
    public string OptionChar { get; set; }

    [Required(ErrorMessage = "optionText is required")]
    public string OptionText { get; set; }
}
