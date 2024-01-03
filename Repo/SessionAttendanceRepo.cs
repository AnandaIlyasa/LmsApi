using LmsApi.Config;

using LmsApi.IRepo;
using LmsApi.Model;
using Microsoft.EntityFrameworkCore;

namespace LmsApi.Repo;

public class SessionAttendanceRepo : ISessionAttendanceRepo
{
    readonly DBContextConfig _context;

    public SessionAttendanceRepo(DBContextConfig context)
    {
        _context = context;
    }

    public SessionAttendance GetSessionAttendanceById(int id)
    {
        var sessionAttendance = _context.SessionAttendances
                                .Where(sa => sa.Id == id)
                                .First();
        return sessionAttendance;
    }

    public List<SessionAttendance> GetSessionAttendanceList(int sessionId)
    {
        var attendanceList = _context.SessionAttendances
                                    .Where(sa => sa.SessionId == sessionId)
                                    .Include(sa => sa.Student)
                                    .OrderBy(sa => sa.CreatedAt)
                                    .ToList();
        return attendanceList;
    }

    public SessionAttendance? GetSessionAttendanceStatus(int sessionId, int studentId)
    {
        var sessionAttendance = _context.SessionAttendances
                                .Where(sa => sa.StudentId == studentId && sa.SessionId == sessionId)
                                .FirstOrDefault();
        return sessionAttendance;
    }
}
