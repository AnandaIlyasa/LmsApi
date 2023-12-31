using LmsApi.Dto.File;
using System.ComponentModel.DataAnnotations;

namespace LmsApi.Dto.Task;

public class SubmissionDetailFilesReqDto
{
    [Required(ErrorMessage = "taskFileId is required")]
    public int TaskFileId { get; set; }
    public List<FileDto> FileList { get; set; }
}
