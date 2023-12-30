using LmsApi.Model;

namespace LmsApi.IService;

public interface ISessionService
{
    SessionAttendance? GetStudentAttendanceStatus(int sessionId);
    SessionAttendance AttendSession(int sessionId);
    List<SessionAttendance> GetSessionAttendanceList(int sessionId);
    void UpdateAttendanceApprovalStatus(SessionAttendance sessionAttendance);
    Session GetSessionAndContentsById(int sessionId);
}
