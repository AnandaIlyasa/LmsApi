using System.ComponentModel.DataAnnotations;

namespace LmsApi.Dto.Task;

public class TaskSubmissionScoreAndNotesReqDto
{
    [Required(ErrorMessage = "grade is required")]
    public double Grade { get; set; }

    [Required(ErrorMessage = "teacherNotes is required")]
    public string TeacherNotes { get; set; }
}
