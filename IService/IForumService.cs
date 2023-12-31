using LmsApi.Dto;
using LmsApi.Dto.Session;
using LmsApi.Model;

namespace LmsApi.IService;

public interface IForumService
{
    List<ForumCommentsResDto> GetForumCommentList(int forumId);
    InsertResDto PostCommentToForum(int forumId, ForumCommentInsertReqDto req);
}
