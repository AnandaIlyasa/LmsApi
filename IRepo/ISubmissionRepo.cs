using LmsApi.Config;
using LmsApi.Model;

namespace LmsApi.IRepo;

public interface ISubmissionRepo
{
    Submission GetSubmissionById(int submissionId);
    Submission GetStudentSubmissionByTask(int studentId, int taskId);
    Submission CreateNewSubmission(Submission submission);
    int UpdateSubmissionGradeAndNotes(Submission submission);
    List<Submission> GetSubmissionListByTask(int taskId);
}
