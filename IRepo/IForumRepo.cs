using LmsApi.Model;

namespace LmsApi.IRepo;

public interface IForumRepo
{
    Forum GetForumBySession(int sessionId);
}
