namespace LmsApi.Dto.Session;

public class SessionForumResDto
{
    public int Id { get; set; }
    public string ForumName { get; set; }
    public string? ForumDescription { get; set; }
    public List<ForumCommentsResDto> CommentList { get; set; }
}
