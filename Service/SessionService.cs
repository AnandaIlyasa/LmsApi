using LmsApi.Config;
using LmsApi.Constant;
using LmsApi.Dto;
using LmsApi.Dto.Material;
using LmsApi.Dto.Session;
using LmsApi.IRepo;
using LmsApi.IService;
using LmsApi.Model;
using LmsApi.Repo;
using System.Globalization;

namespace LmsApi.Service;

public class SessionService : ISessionService
{
    readonly ISessionAttendanceRepo _sessionAttendanceRepo;
    readonly ISessionRepo _sessionRepo;
    readonly ISessionMaterialRepo _sessionMaterialRepo;
    readonly ISessionTaskRepo _sessionTaskRepo;
    readonly ISessionMaterialFileRepo _sessionMaterialFileRepo;
    readonly ILMSFileRepo _fileRepo;
    readonly IForumRepo _forumRepo;
    readonly IForumCommentRepo _forumCommentRepo;
    readonly IUserRepo _userRepo;
    readonly BaseRepo _baseRepo;
    readonly IPrincipleService _principleService;

    readonly string IsoDateTimeFormat = "yyyy-MM-dd HH:mm";
    readonly string IsoTimeFormat = "HH:mm";

    public SessionService
    (
        ISessionAttendanceRepo sessionAttendanceRepo,
        ISessionRepo sessionRepo,
        ISessionMaterialRepo sessionMaterialRepo,
        ISessionTaskRepo sessionTaskRepo,
        ISessionMaterialFileRepo sessionMaterialFileRepo,
        ILMSFileRepo fileRepo,
        IForumRepo forumRepo,
        IForumCommentRepo forumCommentRepo,
        IUserRepo userRepo,
        BaseRepo baseRepo,
        IPrincipleService principleService
    )
    {
        _sessionAttendanceRepo = sessionAttendanceRepo;
        _sessionRepo = sessionRepo;
        _sessionMaterialRepo = sessionMaterialRepo;
        _sessionTaskRepo = sessionTaskRepo;
        _sessionMaterialFileRepo = sessionMaterialFileRepo;
        _fileRepo = fileRepo;
        _forumRepo = forumRepo;
        _forumCommentRepo = forumCommentRepo;
        _userRepo = userRepo;
        _baseRepo = baseRepo;
        _principleService = principleService;
    }

    public InsertResDto AttendSession(int sessionId)
    {
        var sessionAttendance = new SessionAttendance()
        {
            StudentId = _principleService.GetLoginId(),
            SessionId = sessionId,
            IsApproved = false,
        };
        sessionAttendance = _baseRepo.CreateOrUpdateEntry(sessionAttendance);

        var response = new InsertResDto()
        {
            Id = sessionAttendance.Id,
            Message = "Attend success, please wait for teacher approval",
        };
        return response;
    }

    public SessionDetailResDto GetSessionContentsById(int sessionId)
    {
        var userId = _principleService.GetLoginId();
        var user = _userRepo.GetUserById(userId);

        var session = _sessionRepo.GetSessionById(sessionId);
        var materialsRes = GetSessionMaterialsRes(sessionId);
        var tasksRes = GetSessionTasksRes(sessionId);
        var forumRes = GetSessionForumRes(sessionId);

        SessionDetailResDto response;
        if (user.Role.RoleCode == RoleCode.Student)
        {
            var sessionAttendance = _sessionAttendanceRepo.GetSessionAttendanceStatus(sessionId, userId);
            if (sessionAttendance == null)
            {
                throw new Exception("You need to attend the session to be able to see session contents");
            }
            else if (sessionAttendance.IsApproved == false)
            {
                throw new Exception("Wait for teacher approval to be able to see session contents");
            }
            else
            {
                response = new SessionDetailResDto()
                {
                    SessionName = session.SessionName,
                    SessionDescription = session.SessionDescription,
                    StartTime = session.StartTime.ToString(IsoTimeFormat),
                    EndTime = session.EndTime.ToString(IsoTimeFormat),
                    AttendanceApproved = sessionAttendance.IsApproved,
                    Forum = forumRes,
                    MaterialList = materialsRes,
                    TaskList = tasksRes,
                };
            }
        }
        else
        {
            var attendancesRes = GetSessionAttendancesRes(sessionId);
            response = new SessionDetailResDto()
            {
                SessionName = session.SessionName,
                SessionDescription = session.SessionDescription,
                StartTime = session.StartTime.ToString(IsoTimeFormat),
                EndTime = session.EndTime.ToString(IsoTimeFormat),
                AttendanceList = attendancesRes,
                Forum = forumRes,
                MaterialList = materialsRes,
                TaskList = tasksRes,
            };
        }

        return response;
    }

    private List<SessionAttendancesResDto> GetSessionAttendancesRes(int sessionId)
    {
        var attendanceList = _sessionAttendanceRepo.GetSessionAttendanceList(sessionId);
        var attendanceListRes = attendanceList
                                .Select(a =>
                                    new SessionAttendancesResDto()
                                    {
                                        Id = a.Id,
                                        StudentFullName = a.Student.FullName,
                                        IsApproved = a.IsApproved,
                                        CreatedAt = a.CreatedAt.ToString(IsoDateTimeFormat),
                                    }
                                )
                                .ToList();

        return attendanceListRes;
    }

    private List<SessionMaterialsResDto> GetSessionMaterialsRes(int sessionId)
    {
        var materialList = _sessionMaterialRepo.GetMaterialListBySession(sessionId);
        foreach (var material in materialList)
        {
            var materialFileList = _sessionMaterialFileRepo.GetSessionMaterialFileListByMaterial(material.Id);
            material.MaterialFileList = materialFileList;
        }

        var materialListRes = materialList
                            .Select(m =>
                            {
                                var materialFileListRes = m.MaterialFileList?
                                                        .Select(mf =>
                                                            new MaterialFileResDto()
                                                            {
                                                                FileName = mf.FileName,
                                                                FileId = mf.FileId,
                                                            }
                                                        )
                                                        .ToList();
                                var materialRes = new SessionMaterialsResDto()
                                {
                                    MaterialName = m.MaterialName,
                                    MaterialDescription = m.MaterialDescription,
                                    MaterialFileList = materialFileListRes,
                                };
                                return materialRes;
                            })
                            .ToList();

        return materialListRes;
    }

    private List<SessionTasksResDto> GetSessionTasksRes(int sessionId)
    {
        var taskList = _sessionTaskRepo.GetTaskListBySession(sessionId);
        var taskListRes = taskList
                        .Select(t =>
                            new SessionTasksResDto()
                            {
                                Id = t.Id,
                                TaskName = t.TaskName,
                                TaskDescription = t.TaskDescription,
                                Duration = t.Duration,
                            }
                        )
                        .ToList();

        return taskListRes;
    }

    private SessionForumResDto GetSessionForumRes(int sessionId)
    {
        var forum = _forumRepo.GetForumBySession(sessionId);
        var commentList = _forumCommentRepo.GetForumCommentListByForum(sessionId);
        var commentListRes = commentList
                            .Select(fc =>
                                new ForumCommentsResDto()
                                {
                                    UserId = fc.UserId,
                                    FullName = fc.User.FullName,
                                    CommentContent = fc.CommentContent,
                                    CreatedAt = fc.CreatedAt.ToString(IsoDateTimeFormat),
                                }
                            )
                            .ToList();

        var forumRes = new SessionForumResDto()
        {
            Id = forum.Id,
            ForumName = forum.ForumName,
            ForumDescription = forum.ForumDescription,
            CommentList = commentListRes,
        };

        return forumRes;
    }

    public UpdateResDto ApproveAttendance(int sessionAttendanceId)
    {
        var sessionAttendance = _sessionAttendanceRepo.GetSessionAttendanceById(sessionAttendanceId);
        sessionAttendance.IsApproved = true;
        _baseRepo.CreateOrUpdateEntry(sessionAttendance);

        var response = new UpdateResDto()
        {
            Message = "Attendance approval status successfully updated",
        };
        return response;
    }

    public InsertResDto CreateSession(SessionInsertReqDto req)
    {
        InsertResDto response;
        using (var context = new DBContextConfig())
        {
            using (var trx = context.Database.BeginTransaction())
            {
                try
                {
                    TimeOnly startTime;
                    var success = TimeOnly.TryParseExact(req.StartTime, IsoTimeFormat, CultureInfo.CurrentCulture, DateTimeStyles.None, out startTime);
                    if (success == false)
                    {
                        throw new Exception("Error parsing startTime, please use ISO format");
                    }

                    TimeOnly endTime;
                    success = TimeOnly.TryParseExact(req.StartTime, IsoTimeFormat, CultureInfo.CurrentCulture, DateTimeStyles.None, out endTime);
                    if (success == false)
                    {
                        throw new Exception("Error parsing endTime, please use ISO format");
                    }

                    var session = new Session()
                    {
                        LearningId = req.LearningId,
                        SessionName = req.SessionName,
                        SessionDescription = req.SessionDescription,
                        StartTime = startTime,
                        EndTime = endTime,
                    };
                    session = _baseRepo.CreateOrUpdateEntry(session);

                    var forum = new Forum()
                    {
                        SessionId = session.Id,
                        ForumName = req.ForumName,
                        ForumDescription = req.ForumDescription,
                    };
                    _baseRepo.CreateOrUpdateEntry(forum);

                    response = new InsertResDto()
                    {
                        Id = session.Id,
                        Message = "Session successfully created",
                    };

                    trx.Commit();
                }
                catch
                {
                    trx.Rollback();
                    throw;
                }
            }
        }

        return response;
    }

    public InsertResDto CreateMaterial(int sessionId, MaterialInsertReqDto req)
    {
        InsertResDto response;
        using (var context = new DBContextConfig())
        {
            using (var trx = context.Database.BeginTransaction())
            {
                try
                {
                    var material = new SessionMaterial()
                    {
                        SessionId = sessionId,
                        MaterialName = req.MaterialName,
                        MaterialDescription = req.MaterialDescription,
                    };
                    material = _baseRepo.CreateOrUpdateEntry(material);

                    foreach (var item in req.MaterialFileList)
                    {
                        var file = new LMSFile()
                        {
                            FileContent = item.File.FileContent,
                            FileExtension = item.File.FileExtension,
                        };
                        file = _baseRepo.CreateOrUpdateEntry(file);

                        var materialFile = new SessionMaterialFile()
                        {
                            FileId = file.Id,
                            FileName = item.FileName,
                            MaterialId = material.Id,
                        };
                        _baseRepo.CreateOrUpdateEntry(materialFile);
                    }

                    response = new InsertResDto()
                    {
                        Id = material.Id,
                        Message = "Material successfully created",
                    };

                    trx.Commit();
                }
                catch
                {
                    trx.Rollback();
                    throw;
                }
            }
        }

        return response;
    }
}
