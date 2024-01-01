using LmsApi.Model;

namespace LmsApi.IRepo;

public interface IForumRepo
{
    Forum CreateForum(Forum forum);
    Forum GetForumBySession(int sessionId);
}
