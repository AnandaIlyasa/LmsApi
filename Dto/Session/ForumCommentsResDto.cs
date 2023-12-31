namespace LmsApi.Dto.Session;

public class ForumCommentsResDto
{
    public int UserId { get; set; }
    public string FullName { get; set; }
    public string CommentContent { get; set; }
    public string CreatedAt { get; set; }
}
