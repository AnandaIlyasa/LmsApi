using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
namespace LmsApi.Model;

[Table("t_r_session_attendance")]
[Index(nameof(StudentId), nameof(SessionId), IsUnique = true, Name = "session_attendance_ck")]
public class SessionAttendance : BaseModel
{
    [Column("is_approved")]
    public bool IsApproved { get; set; }

    [Column("student_id")]
    public int StudentId { get; set; }

    [ForeignKey(nameof(StudentId))]
    public User Student { get; set; }

    [Column("session_id")]
    public int SessionId { get; set; }

    [ForeignKey(nameof(SessionId))]
    public Session Session { get; set; }
}
