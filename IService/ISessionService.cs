using LmsApi.Dto;
using LmsApi.Dto.Material;
using LmsApi.Dto.Session;
using LmsApi.Model;

namespace LmsApi.IService;

public interface ISessionService
{
    InsertResDto CreateSession(SessionInsertReqDto req);
    InsertResDto CreateMaterial(int sessionId, MaterialInsertReqDto req);
    InsertResDto AttendSession(int sessionId);
    UpdateResDto ApproveAttendance(int sessionAttendanceId);
    SessionDetailResDto GetSessionContentsById(int sessionId);
}
