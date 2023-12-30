using LmsApi.IRepo;
using LmsApi.IService;
using LmsApi.Model;

namespace LmsApi.Service;

public class ForumService : IForumService
{
    readonly IForumCommentRepo _forumCommentRepo;
    readonly IPrincipleService _principleService;

    public ForumService(IForumCommentRepo forumCommentRepo, IPrincipleService principleService)
    {
        _forumCommentRepo = forumCommentRepo;
        _principleService = principleService;
    }

    public List<ForumComment> GetForumCommentList(int forumId)
    {
        var commentList = _forumCommentRepo.GetForumCommentListByForum(forumId);
        commentList = commentList
                    .OrderBy(c => c.CreatedAt)
                    .ToList();
        return commentList;
    }

    public ForumComment PostCommentToForum(ForumComment forumComment)
    {
        forumComment.UserId = _principleService.GetLoginId();
        forumComment.CreatedBy = _principleService.GetLoginId();
        forumComment.CreatedAt = DateTime.Now;
        var newComment = _forumCommentRepo.CreateNewComment(forumComment);
        return newComment;
    }
}
