using LmsApi.Model;

namespace LmsApi.IRepo;

public interface IForumCommentRepo
{
    List<ForumComment> GetForumCommentListByForum(int forumId);
}
