using System.ComponentModel.DataAnnotations.Schema;

namespace LmsApi.Model;

[Table("t_r_forum_comment")]
public class ForumComment : BaseModel
{
    [Column("comment_content")]
    public string CommentContent { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; }

    [Column("forum_id")]
    public int ForumId { get; set; }

    [ForeignKey(nameof(ForumId))]
    public Forum Forum { get; set; }
}
