using LmsApi.Config;
using LmsApi.Model;

namespace LmsApi.IRepo;

public interface ISessionRepo
{
    Session GetSessionById(int sessionId);
    List<Session> GetSessionListByLearning(int learningId);
}
