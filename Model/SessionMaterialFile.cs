using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LmsApi.Model;

[Table("t_r_session_material_file")]
public class SessionMaterialFile : BaseModel
{
    [Column("file_name"), MaxLength(30)]
    public string? FileName { get; set; }

    [Column("file_id")]
    public int FileId { get; set; }

    [ForeignKey(nameof(FileId))]
    public LMSFile File { get; set; }

    [Column("material_id")]
    public int MaterialId { get; set; }

    [ForeignKey(nameof(MaterialId))]
    public SessionMaterial Material { get; set; }
}
