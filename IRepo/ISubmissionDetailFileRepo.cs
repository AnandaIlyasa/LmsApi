using LmsApi.Model;

namespace LmsApi.IRepo;

public interface ISubmissionDetailFileRepo
{
    SubmissionDetailFile CreateNewSubmissionDetailFile(SubmissionDetailFile submissionDetailFile);
    List<SubmissionDetailFile> GetStudentSubmissionDetailFileByTask(int taskId, int studentId);
}
