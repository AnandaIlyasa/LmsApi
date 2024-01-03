using LmsApi.Model;

namespace LmsApi.IRepo;

public interface ITaskQuestionRepo
{
    List<TaskQuestion> GetQuestionListByTask(int taskId);
}
