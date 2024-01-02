using LmsApi.Model;

namespace LmsApi.IRepo;

public interface ITaskQuestionRepo
{
    TaskQuestion CreateQuestion(TaskQuestion question);
    List<TaskQuestion> GetQuestionListByTask(int taskId);
}
