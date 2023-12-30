using LmsApi.Model;

namespace LmsApi.IRepo;

public interface ISubmissionDetailQuestionRepo
{
    SubmissionDetailQuestion CreateNewSubmissionDetailQuestion(SubmissionDetailQuestion submissionDetail);
    List<SubmissionDetailQuestion> GetStudentSubmissionDetailQuestionByTask(int taskId, int studentId);
}
