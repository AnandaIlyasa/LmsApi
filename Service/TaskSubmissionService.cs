using LmsApi.Config;

using LmsApi.IRepo;
using LmsApi.IService;
using LmsApi.Model;

namespace LmsApi.Service;

public class TaskSubmissionService : ITaskSubmissionService
{
    readonly ISubmissionRepo _submissionRepo;
    readonly ISubmissionDetailQuestionRepo _submissionDetailQuestionRepo;
    readonly ISubmissionDetailFileRepo _submissionDetailFileRepo;
    readonly ILMSFileRepo _fileRepo;
    readonly IPrincipleService _principleService;

    public TaskSubmissionService
    (
        ISubmissionRepo submissionRepo,
        ISubmissionDetailQuestionRepo submissionDetailRepo,
        ISubmissionDetailFileRepo submissionDetailFileRepo,
        ILMSFileRepo fileRepo,
        IPrincipleService principleService
    )
    {
        _submissionRepo = submissionRepo;
        _submissionDetailQuestionRepo = submissionDetailRepo;
        _submissionDetailFileRepo = submissionDetailFileRepo;
        _fileRepo = fileRepo;
        _principleService = principleService;
    }

    public Submission GetStudentSubmissionByTask(int studentId, int taskId)
    {
        var submission = _submissionRepo.GetStudentSubmissionByTask(studentId, taskId);

        var submissionQuestionList = _submissionDetailQuestionRepo.GetStudentSubmissionDetailQuestionByTask(taskId, studentId);
        submission.SubmissionDetailQuestionList = submissionQuestionList;

        var submissionFileList = _submissionDetailFileRepo.GetStudentSubmissionDetailFileByTask(taskId, studentId);
        submission.SubmissionDetailFileList = submissionFileList;

        return submission;
    }

    public List<Submission> GetStudentSubmissionListBySession(int sessionId)
    {
        var submissionList = _submissionRepo.GetStudentSubmissionListBySession(sessionId, _principleService.GetLoginId());
        return submissionList;
    }

    public List<Submission> GetSubmissionListBySession(int sessionId)
    {
        var submissionList = _submissionRepo.GetSubmissionListBySession(sessionId);

        foreach (var submission in submissionList)
        {
            var submissionQuestionList = _submissionDetailQuestionRepo.GetStudentSubmissionDetailQuestionByTask(submission.TaskId, submission.StudentId);
            submission.SubmissionDetailQuestionList = submissionQuestionList;
        }

        foreach (var submission in submissionList)
        {
            var submissionFileList = _submissionDetailFileRepo.GetStudentSubmissionDetailFileByTask(submission.TaskId, submission.StudentId);
            submission.SubmissionDetailFileList = submissionFileList;
        }

        return submissionList;
    }

    public void InsertScoreAndNotes(Submission submission)
    {
        submission.UpdatedAt = DateTime.Now;
        submission.UpdatedBy = _principleService.GetLoginId();
        _submissionRepo.UpdateSubmissionGradeAndNotes(submission);
    }

    public void SubmitTask(Submission submission)
    {
        using (var context = new DBContextConfig())
        {
            var trx = context.Database.BeginTransaction();

            submission.StudentId = _principleService.GetLoginId();
            submission.CreatedBy = _principleService.GetLoginId();
            submission.CreatedAt = DateTime.Now;
            var insertedSubmission = _submissionRepo.CreateNewSubmission(submission);

            foreach (var questionSubmission in submission.SubmissionDetailQuestionList)
            {
                questionSubmission.SubmissionId = insertedSubmission.Id;
                questionSubmission.CreatedBy = _principleService.GetLoginId();
                questionSubmission.CreatedAt = DateTime.Now;
                _submissionDetailQuestionRepo.CreateNewSubmissionDetailQuestion(questionSubmission);
            }

            foreach (var fileSubmission in submission.SubmissionDetailFileList)
            {
                fileSubmission.File.CreatedBy = _principleService.GetLoginId();
                fileSubmission.File.CreatedAt = DateTime.Now;
                var insertedFile = _fileRepo.CreateNewFile(fileSubmission.File);

                fileSubmission.FileId = insertedFile.Id;
                fileSubmission.SubmissionId = insertedSubmission.Id;
                fileSubmission.CreatedBy = _principleService.GetLoginId();
                fileSubmission.CreatedAt = DateTime.Now;
                _submissionDetailFileRepo.CreateNewSubmissionDetailFile(fileSubmission);
            }

            trx.Commit();
        }
    }
}
