using LmsApi.Dto.File;

namespace LmsApi.Dto.Session;

public class SubmissionsResDto
{
    public int? Id { get; set; }
    public string? StudentFullName { get; set; }
    public string CreatedAt { get; set; }
    public double? MultipleChoiceScore { get; set; }
    public double? FinalScore { get; set; }
    public string? TeacherNotes { get; set; }
    public List<SubmissionDetailQuestionsResDto>? SubmissionDetailQuestionList { get; set; }
    public List<SubmissionDetailFilesResDto>? SubmissionDetailFileList { get; set; }
}
