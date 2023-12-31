using System.ComponentModel.DataAnnotations;

namespace LmsApi.Dto.Session;

public class ForumCommentInsertReqDto
{
    [Required(ErrorMessage = "commentContent is required")]
    public string CommentContent { get; set; }
}
