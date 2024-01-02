using LmsApi.Model;

namespace LmsApi.IRepo;

public interface ITaskRepo
{
    LMSTask CreateTask(LMSTask task);
}
