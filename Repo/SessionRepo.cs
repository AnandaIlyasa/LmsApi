using LmsApi.Config;

using LmsApi.IRepo;
using LmsApi.Model;

namespace LmsApi.Repo;

public class SessionRepo : ISessionRepo
{
    readonly DBContextConfig _context;

    public SessionRepo(DBContextConfig context)
    {
        _context = context;
    }

    public Session CreateSession(Session session)
    {
        _context.Sessions.Add(session);
        _context.SaveChanges();
        return session;
    }

    public Session GetSessionById(int sessionId)
    {
        var session = _context.Sessions
                    .Where(s => s.Id == sessionId)
                    .First();
        return session;
    }

    public List<Session> GetSessionListByLearning(int learningId)
    {
        var sessionList = _context.Sessions
                        .Where(s => s.LearningId == learningId)
                        .ToList();
        return sessionList;
    }
}
