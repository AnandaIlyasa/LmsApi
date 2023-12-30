using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LmsApi.Model;

[Table("t_r_task_file")]
public class TaskFile : BaseModel
{
    [Column("file_name"), MaxLength(30)]
    public string? FileName { get; set; }

    [Column("file_id")]
    public int FileId { get; set; }

    [ForeignKey(nameof(FileId))]
    public LMSFile File { get; set; }
}
