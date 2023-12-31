﻿using LmsApi.Dto;
using LmsApi.Dto.Material;
using LmsApi.Dto.Session;
using LmsApi.Dto.Task;
using LmsApi.IService;
using Microsoft.AspNetCore.Mvc;

namespace LmsApi.Controllers;

[ApiController]
[Route("sessions")]
public class SessionController : ControllerBase
{
    readonly ISessionService _sessionService;
    readonly IForumService _forumService;

    public SessionController(ISessionService sessionService, IForumService forumService)
    {
        _sessionService = sessionService;
        _forumService = forumService;
    }

    [HttpGet("{sessionId}")]
    public SessionDetailResDto GetSessionDetail(int sessionId)
    {
        var response = _sessionService.GetSessionContentsById(sessionId);
        return response;
    }

    [HttpPost]
    public InsertResDto CreateSession(SessionInsertReqDto req)
    {
        var response = _sessionService.CreateSession(req);
        return response;
    }

    [HttpPost("{sessionId}/materials")]
    public InsertResDto CreateMaterial(int sessionId, [FromBody] MaterialInsertReqDto req)
    {
        var response = _sessionService.CreateMaterial(sessionId, req);
        return response;
    }

    [HttpPost("{sessionId}")]
    public InsertResDto AttendSession(int sessionId)
    {
        var response = _sessionService.AttendSession(sessionId);
        return response;
    }

    [HttpPatch("attendances/{sessionAttendanceId}")]
    public UpdateResDto ApproveAttendance(int sessionAttendanceId)
    {
        var response = _sessionService.ApproveAttendance(sessionAttendanceId);
        return response;
    }

    [HttpPost("forums/{forumId}/comments")]
    public InsertResDto CreateForumComment(int forumId, [FromBody] ForumCommentInsertReqDto req)
    {
        var response = _forumService.PostCommentToForum(forumId, req);
        return response;
    }

    [HttpGet("forums/{forumId}/comments")]
    public List<ForumCommentsResDto> GetCommentListByForum(int forumId)
    {
        var response = _forumService.GetForumCommentList(forumId);
        return response;
    }
}
