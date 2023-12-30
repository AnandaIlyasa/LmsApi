using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace LmsApi.Model;

[Table("t_r_submission_detail_file")]
[Index(nameof(SubmissionId), nameof(FileId), IsUnique = true, Name = "submission_file_ck")]
public class SubmissionDetailFile : BaseModel
{
    [Column("submission_id")]
    public int SubmissionId { get; set; }

    [ForeignKey(nameof(SubmissionId))]
    public Submission Submission { get; set; }

    [Column("file_id")]
    public int FileId { get; set; }

    [ForeignKey(nameof(FileId))]
    public LMSFile File { get; set; }

    [Column("task_file_id")]
    public int TaskFileId { get; set; }

    [ForeignKey(nameof(TaskFileId))]
    public TaskFile TaskFile { get; set; }
}
