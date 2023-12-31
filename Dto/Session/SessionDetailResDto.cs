namespace LmsApi.Dto.Session;

public class SessionDetailResDto
{
    public string SessionName { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public string? SessionDescription { get; set; }
    public bool? AttendanceApproved { get; set; } // for student
    public List<SessionAttendancesResDto>? AttendanceList { get; set; } // for teacher
    public SessionForumResDto? Forum { get; set; }
    public List<SessionTasksResDto>? TaskList { get; set; }
    public List<SessionMaterialsResDto>? MaterialList { get; set; }
}
