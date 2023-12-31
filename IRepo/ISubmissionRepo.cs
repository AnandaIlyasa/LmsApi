using LmsApi.Config;
using LmsApi.Model;

namespace LmsApi.IRepo;

public interface ISubmissionRepo
{
    Submission GetStudentSubmissionByTask(int studentId, int taskId);
    Submission CreateNewSubmission(Submission submission);
    int UpdateSubmissionGradeAndNotes(Submission submission);
    List<Submission> GetStudentSubmissionListBySession(int sessionId, int studentId);
    List<Submission> GetSubmissionListByTask(int taskId);
}
