using LmsApi.Dto.Session;

namespace LmsApi.Dto.Task;

public class TaskDetailsStudentSubmissionResDto
{
    public SubmissionsResDto? Submission { get; set; }
    public List<TaskQuestionsDto> TaskQuestionList { get; set; }
    public List<TaskFileResDto> TaskFileList { get; set; }
}
