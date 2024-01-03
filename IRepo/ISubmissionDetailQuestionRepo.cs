using LmsApi.Model;

namespace LmsApi.IRepo;

public interface ISubmissionDetailQuestionRepo
{
    List<SubmissionDetailQuestion> GetStudentSubmissionDetailQuestionByTask(int taskId, int studentId);
}
