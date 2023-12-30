namespace LmsApi.IRepo;

using LmsApi.Config;
using LmsApi.Model;

public interface ISessionTaskRepo
{
    List<LMSTask> GetTaskListBySession(int sessionId);
}
