using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LmsApi.Model;

[Table("t_m_forum")]
public class Forum : BaseModel
{
    [Column("forum_name"), MaxLength(30)]
    public string ForumName { get; set; }

    [Column("forum_description")]
    public string? ForumDescription { get; set; }

    [Column("session_id")]
    public int SessionId { get; set; }

    [ForeignKey(nameof(SessionId))]
    public Session Session { get; set; }

    [NotMapped]
    public List<ForumComment> CommentList { get; set; } // not mapped
}
