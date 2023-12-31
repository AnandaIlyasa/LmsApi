using System.ComponentModel.DataAnnotations;

namespace LmsApi.Dto.File;

public class FileDto
{
    [Required(ErrorMessage = "fileContent is required")]
    public string FileContent { get; set; }

    [Required(ErrorMessage = "fileExtension is required"), MaxLength(5)]
    public string FileExtension { get; set; }
}
