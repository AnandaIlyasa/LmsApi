namespace LmsApi.IRepo;

using LmsApi.Config;
using LmsApi.Model;

public interface ISessionAttendanceRepo
{
    SessionAttendance? GetSessionAttendanceStatus(int sessionId, int studentId);
    SessionAttendance CreateNewSessionAttendance(SessionAttendance sessionAttendance);
    List<SessionAttendance> GetSessionAttendanceList(int sessionId);
    int UpdateSessionAttendance(SessionAttendance sessionAttendance);
}
