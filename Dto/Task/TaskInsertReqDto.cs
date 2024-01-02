using System.ComponentModel.DataAnnotations;

namespace LmsApi.Dto.Task;

public class TaskInsertReqDto
{
    [Required(ErrorMessage = "sessionId is required")]
    public int SessionId { get; set; }

    [Required(ErrorMessage = "taskName is required"), MaxLength(30)]
    public string TaskName { get; set; }

    public string? TaskDescription { get; set; }

    [Required]
    public int Duration { get; set; }
}
