using LmsApi.Dto.Session;
using LmsApi.Model;

namespace LmsApi.IService;

public interface ITaskService
{
    TaskDetailsSubmissionsResDto GetTaskDetailsAndSubmissions(int taskId);
    void SubmitTask(Submission submission);
    List<Submission> GetStudentSubmissionListBySession(int sessionId);
    Submission GetStudentSubmissionByTask(int studentId, int taskId);
    void InsertScoreAndNotes(Submission submission);
}
