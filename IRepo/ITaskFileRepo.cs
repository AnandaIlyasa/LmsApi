using LmsApi.Model;

namespace LmsApi.IRepo;

public interface ITaskFileRepo
{
    TaskFile CreateTaskFile(TaskFile taskFile);
    List<TaskFile> GetTaskFileList(int taskId);
}
