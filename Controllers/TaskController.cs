using LmsApi.Dto.Session;
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

    [HttpGet("{taskId}")]
    public TaskDetailsSubmissionsResDto GetTaskDetailAndSubmissionList(int taskId)
    {
        var response = _taskService.GetTaskDetailsAndSubmissions(taskId);
        return response;
    }
}
