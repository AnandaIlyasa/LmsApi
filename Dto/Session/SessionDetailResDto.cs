namespace LmsApi.Dto.Session;

public class SessionDetailResDto
{
    public string SessionName { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public string? SessionDescription { get; set; }
    public List<SessionAttendancesResDto> AttendanceList { get; set; }
    public SessionForumResDto Forum { get; set; }
    public List<SessionTasksResDto> TaskList { get; set; }
    public List<SessionMaterialsResDto> MaterialList { get; set; }
}
