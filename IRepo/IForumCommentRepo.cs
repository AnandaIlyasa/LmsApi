using LmsApi.Model;

namespace LmsApi.IRepo;

public interface IForumCommentRepo
{
    ForumComment CreateNewComment(ForumComment forumComment);
    List<ForumComment> GetForumCommentListByForum(int forumId);
}
