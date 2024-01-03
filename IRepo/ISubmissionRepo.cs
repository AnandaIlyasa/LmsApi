using LmsApi.Config;
using LmsApi.Model;

namespace LmsApi.IRepo;

public interface ISubmissionRepo
{
    Submission GetSubmissionById(int submissionId);
    Submission GetStudentSubmissionByTask(int studentId, int taskId);
    List<Submission> GetSubmissionListByTask(int taskId);
}
