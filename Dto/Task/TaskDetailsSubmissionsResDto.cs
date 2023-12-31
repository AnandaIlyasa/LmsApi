using LmsApi.Dto.Session;

namespace LmsApi.Dto.Task;

public class TaskDetailsSubmissionsResDto
{
    public List<SubmissionsResDto> SubmissionList { get; set; }
    public List<TaskQuestionResDto> TaskQuestionList { get; set; }
    public List<TaskFileResDto> TaskFileList { get; set; }
}
