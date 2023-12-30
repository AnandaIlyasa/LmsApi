using LmsApi.Model;

namespace LmsApi.IService;

public interface IForumService
{
    List<ForumComment> GetForumCommentList(int forumId);
    ForumComment PostCommentToForum(ForumComment forumComment);
}
