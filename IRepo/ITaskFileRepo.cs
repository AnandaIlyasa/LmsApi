using LmsApi.Model;

namespace LmsApi.IRepo;

public interface ITaskFileRepo
{
    List<TaskFile> GetTaskFileList(int taskId);
}
