using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
namespace LmsApi.Model;

[Table("t_r_submission")]
[Index(nameof(StudentId), nameof(TaskId), IsUnique = true, Name = "submission_ck")]
public class Submission : BaseModel
{
    [Column("grade")]
    public double? Grade { get; set; }

    [Column("teacher_notes")]
    public string? TeacherNotes { get; set; }

    [Column("student_id")]
    public int StudentId { get; set; }

    [ForeignKey(nameof(StudentId))]
    public User Student { get; set; }

    [Column("task_id")]
    public int TaskId { get; set; }

    [ForeignKey(nameof(TaskId))]
    public LMSTask Task { get; set; }

    [NotMapped]
    public List<SubmissionDetailQuestion> SubmissionDetailQuestionList { get; set; } // not mapped

    [NotMapped]
    public List<SubmissionDetailFile> SubmissionDetailFileList { get; set; } // not mapped
}
