using LmsApi.Dto;
using LmsApi.Dto.Task;
using LmsApi.IService;
using Microsoft.AspNetCore.Mvc;

namespace LmsApi.Controllers;

[ApiController]
[Route("tasks")]
public class TaskController : ControllerBase
{
    readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpPost("{taskId}")]
    public InsertResDto SubmitTask(int taskId, [FromBody] TaskSubmissionDetailsReqDto req)
    {
        var response = _taskService.SubmitTask(taskId, req);
        return response;
    }

    [HttpGet("{taskId}")]
    public TaskDetailsSubmissionsResDto GetTaskDetailAndSubmissionList(int taskId)
    {
        var response = _taskService.GetTaskDetailsAndSubmissions(taskId);
        return response;
    }

    [HttpGet("{taskId}/students")]
    public TaskDetailsStudentSubmissionResDto GetTaskDetailAndStudentSubmission(int taskId)
    {
        var response = _taskService.GetTaskDetailAndStudentSubmissionByTask(taskId);
        return response;
    }

    [HttpPatch("submissions/{submissionId}")]
    public UpdateResDto InsertScoreAndNotes(int submissionId, [FromBody] TaskSubmissionScoreAndNotesReqDto req)
    {
        var response = _taskService.InsertScoreAndNotes(submissionId, req);
        return response;
    }
}
