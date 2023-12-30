using LmsApi.Model;

namespace LmsApi.IService;

public interface ITaskSubmissionService
{
    void SubmitTask(Submission submission);
    List<Submission> GetStudentSubmissionListBySession(int sessionId);
    Submission GetStudentSubmissionByTask(int studentId, int taskId);
    List<Submission> GetSubmissionListBySession(int sessionId);
    void InsertScoreAndNotes(Submission submission);
}
