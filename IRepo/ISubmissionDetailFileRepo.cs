using LmsApi.Model;

namespace LmsApi.IRepo;

public interface ISubmissionDetailFileRepo
{
    List<SubmissionDetailFile> GetStudentSubmissionDetailFileByTask(int taskId, int studentId);
}
