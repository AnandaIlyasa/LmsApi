namespace LmsApi.IRepo;

using LmsApi.Config;
using LmsApi.Model;

public interface ISessionAttendanceRepo
{
    SessionAttendance GetSessionAttendanceById(int id);
    SessionAttendance? GetSessionAttendanceStatus(int sessionId, int studentId);
    List<SessionAttendance> GetSessionAttendanceList(int sessionId);
}
