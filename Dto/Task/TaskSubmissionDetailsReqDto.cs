using System.ComponentModel.DataAnnotations;

namespace LmsApi.Dto.Task;

public class TaskSubmissionDetailsReqDto
{
    public List<SubmissionDetailFilesReqDto> SubmissionDetailFiles { get; set; }

    public List<SubmissionDetailQuestionsReqDto> SubmissionDetailQuestions { get; set; }
}
