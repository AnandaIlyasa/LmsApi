using LmsApi.Model;

namespace LmsApi.IRepo;

public interface ITaskDetailRepo
{
    TaskDetail CreateTaskDetail(TaskDetail taskDetail);
}
