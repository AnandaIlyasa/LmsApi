namespace LmsApi.Dto.Session;

public class TaskDetailsSubmissionsResDto
{
    public List<SubmissionsResDto> SubmissionList { get; set; }
    public List<TaskQuestionResDto> TaskQuestionList { get; set; }
    public List<TaskFileResDto> TaskFileList { get; set; }
}
