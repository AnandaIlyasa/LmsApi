using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LmsApi.Model;

[Table("t_m_session_material")]
public class SessionMaterial : BaseModel
{
    [Column("material_name"), MaxLength(30)]
    public string MaterialName { get; set; }

    [Column("material_description")]
    public string? MaterialDescription { get; set; }

    [Column("session_id")]
    public int SessionId { get; set; }

    [ForeignKey(nameof(SessionId))]
    public Session Session { get; set; }

    [NotMapped]
    public List<SessionMaterialFile>? MaterialFileList { get; set; } // not mapped
}
