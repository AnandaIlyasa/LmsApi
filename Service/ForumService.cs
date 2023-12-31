using LmsApi.Dto;
using LmsApi.Dto.Session;
using LmsApi.IRepo;
using LmsApi.IService;
using LmsApi.Model;

namespace LmsApi.Service;

public class ForumService : IForumService
{
    readonly IForumCommentRepo _forumCommentRepo;
    readonly IPrincipleService _principleService;

    readonly string IsoDateTimeFormat = "yyyy-MM-dd HH:mm";

    public ForumService(IForumCommentRepo forumCommentRepo, IPrincipleService principleService)
    {
        _forumCommentRepo = forumCommentRepo;
        _principleService = principleService;
    }

    public List<ForumCommentsResDto> GetForumCommentList(int forumId)
    {
        var commentList = _forumCommentRepo.GetForumCommentListByForum(forumId);
        var response = commentList
                    .Select(c =>
                        new ForumCommentsResDto()
                        {
                            UserId = c.UserId,
                            CommentContent = c.CommentContent,
                            FullName = c.User.FullName,
                            CreatedAt = c.CreatedAt.ToString(IsoDateTimeFormat),
                        }
                    )
                    .OrderBy(c => c.CreatedAt)
                    .ToList();
        return response;
    }

    public InsertResDto PostCommentToForum(int forumId, ForumCommentInsertReqDto req)
    {
        var forumComment = new ForumComment()
        {
            ForumId = forumId,
            CommentContent = req.CommentContent,
            UserId = _principleService.GetLoginId(),
            CreatedBy = _principleService.GetLoginId(),
            CreatedAt = DateTime.Now,
        };
        var insertedComment = _forumCommentRepo.CreateNewComment(forumComment);

        var response = new InsertResDto()
        {
            Id = insertedComment.Id,
            Message = "Comment successfully created",
        };
        return response;
    }
}
