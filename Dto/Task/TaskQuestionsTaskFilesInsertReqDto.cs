namespace LmsApi.Dto.Task;

public class TaskQuestionsTaskFilesInsertReqDto
{
    public List<TaskQuestionsDto> TaskQuestionList { get; set; }
    public List<TaskFileInsertReqDto> TaskFileList { get; set; }
}
