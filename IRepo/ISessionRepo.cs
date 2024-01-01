using LmsApi.Config;
using LmsApi.Model;

namespace LmsApi.IRepo;

public interface ISessionRepo
{
    Session CreateSession(Session session);
    Session GetSessionById(int sessionId);
    List<Session> GetSessionListByLearning(int learningId);
}
