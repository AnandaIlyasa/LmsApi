using LmsApi.Dto;
using LmsApi.Dto.Session;
using LmsApi.Model;

namespace LmsApi.IService;

public interface ISessionService
{
    InsertResDto CreateSession(SessionInsertReqDto req);
    SessionAttendance? GetStudentAttendanceStatus(int sessionId);
    InsertResDto AttendSession(int sessionId);
    UpdateResDto ApproveAttendance(int sessionAttendanceId);
    SessionDetailResDto GetSessionContentsById(int sessionId);
}
