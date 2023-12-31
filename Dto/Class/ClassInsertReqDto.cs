using LmsApi.Dto.File;
using System.ComponentModel.DataAnnotations;

namespace LmsApi.Dto.Class;

public class ClassInsertReqDto
{
    [Required(ErrorMessage = "teacherId is required")]
    public int TeacherId { get; set; }

    [Required(ErrorMessage = "classCode is required"), MaxLength(10)]
    public string ClassCode { get; set; }

    [Required(ErrorMessage = "className is required"), MaxLength(50)]
    public string ClassName { get; set; }

    [Required(ErrorMessage = "classDescription is required")]
    public string ClassDescription { get; set; }

    [Required(ErrorMessage = "classImage is required")]
    public FileDto ClassImage { get; set; }
}
