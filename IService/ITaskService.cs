using LmsApi.Dto;
using LmsApi.Dto.Task;
using LmsApi.Model;

namespace LmsApi.IService;

public interface ITaskService
{
    TaskDetailsSubmissionsResDto GetTaskDetailsAndSubmissions(int taskId);
    InsertResDto SubmitTask(int taskId, TaskSubmissionDetailsReqDto req);
    TaskDetailsStudentSubmissionResDto GetTaskDetailAndStudentSubmissionByTask(int taskId);
    UpdateResDto InsertScoreAndNotes(int submissionId, TaskSubmissionScoreAndNotesReqDto req);
}
