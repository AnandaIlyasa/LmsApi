using System.ComponentModel.DataAnnotations;

namespace LmsApi.Dto.Session;

public class SessionInsertReqDto
{
    public int LearningId { get; set; }

    [Required(ErrorMessage = "sessionName is required"), MaxLength(50)]
    public string SessionName { get; set; }

    public string SessionDescription { get; set; }

    [Required(ErrorMessage = "startTime is required")]
    public string StartTime { get; set; }

    [Required(ErrorMessage = "endTime is required")]
    public string EndTime { get; set; }

    [Required(ErrorMessage = "forumName is required"), MaxLength(30)]
    public string ForumName { get; set; }

    public string ForumDescription { get; set; }
}
