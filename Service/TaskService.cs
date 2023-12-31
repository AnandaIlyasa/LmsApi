using LmsApi.Config;
using LmsApi.Constant;
using LmsApi.Dto.Session;
using LmsApi.IRepo;
using LmsApi.IService;
using LmsApi.Model;
using LmsApi.Repo;
using System.Threading.Tasks;

namespace LmsApi.Service;

public class TaskService : ITaskService
{
    readonly ISubmissionRepo _submissionRepo;
    readonly ISubmissionDetailQuestionRepo _submissionDetailQuestionRepo;
    readonly ISubmissionDetailFileRepo _submissionDetailFileRepo;
    readonly ILMSFileRepo _fileRepo;
    readonly ITaskQuestionRepo _questionRepo;
    readonly ITaskFileRepo _taskFileRepo;
    readonly ITaskMultipleChoiceOptionRepo _multipleChoiceOptionRepo;
    readonly IPrincipleService _principleService;

    readonly string IsoDateTimeFormat = "yyyy-MM-dd HH:mm";

    public TaskService
    (
        ISubmissionRepo submissionRepo,
        ISubmissionDetailQuestionRepo submissionDetailRepo,
        ISubmissionDetailFileRepo submissionDetailFileRepo,
        ILMSFileRepo fileRepo,
        ITaskQuestionRepo questionRepo,
        ITaskFileRepo taskFileRepo,
        ITaskMultipleChoiceOptionRepo multipleChoiceOptionRepo,
        IPrincipleService principleService
    )
    {
        _submissionRepo = submissionRepo;
        _submissionDetailQuestionRepo = submissionDetailRepo;
        _submissionDetailFileRepo = submissionDetailFileRepo;
        _fileRepo = fileRepo;
        _questionRepo = questionRepo;
        _taskFileRepo = taskFileRepo;
        _multipleChoiceOptionRepo = multipleChoiceOptionRepo;
        _principleService = principleService;
    }

    public TaskDetailsSubmissionsResDto GetTaskDetailsAndSubmissions(int taskId)
    {
        var questionListRes = GetQuestionsRes(taskId);
        var multipleChoiceQuestionCount = 0;
        foreach (var question in questionListRes)
            if (question.QuestionType == QuestionType.MultipleChoice) multipleChoiceQuestionCount++;

        var taskFileListRes = GetTaskFilesRes(taskId);
        var submissionListRes = GetSubmissionsResByTask(taskId, multipleChoiceQuestionCount);

        var response = new TaskDetailsSubmissionsResDto()
        {
            TaskQuestionList = questionListRes,
            TaskFileList = taskFileListRes,
            SubmissionList = submissionListRes,
        };

        return response;
    }

    private List<TaskQuestionResDto> GetQuestionsRes(int taskId)
    {
        var questionList = _questionRepo.GetQuestionListByTask(taskId);
        foreach (var question in questionList)
        {
            if (question.QuestionType == QuestionType.MultipleChoice)
            {
                var optionList = _multipleChoiceOptionRepo.GetMultipleChoiceOptionListByQuestion(question.Id);
                optionList = optionList
                            .OrderBy(o => o.OptionChar)
                            .ToList();
                question.OptionList = optionList;
            }
        }

        var questionListRes = questionList
                            .Select(q =>
                            {
                                List<MultipleChoiceOptionsResDto>? optionListRes = null;
                                if (q.OptionList != null && q.OptionList.Count > 0)
                                {
                                    optionListRes = q.OptionList
                                                    .Select(o =>
                                                        new MultipleChoiceOptionsResDto()
                                                        {
                                                            Id = o.Id,
                                                            OptionChar = o.OptionChar,
                                                            OptionText = o.OptionText,
                                                        }
                                                    )
                                                    .ToList();
                                }

                                var questionRes = new TaskQuestionResDto()
                                {
                                    Id = q.Id,
                                    QuestionContent = q.QuestionContent,
                                    QuestionType = q.QuestionType,
                                    OptionList = optionListRes,
                                };

                                return questionRes;
                            })
                            .OrderByDescending(qr => qr.QuestionType)
                            .ToList();

        return questionListRes;
    }

    private List<TaskFileResDto> GetTaskFilesRes(int taskId)
    {
        var taskFileList = _taskFileRepo.GetTaskFileList(taskId);
        var taskFileListRes = taskFileList
                            .Select(tf =>
                                new TaskFileResDto()
                                {
                                    Id = tf.Id,
                                    FileName = tf.FileName,
                                    FileId = tf.FileId,
                                }
                            )
                            .ToList();

        return taskFileListRes;
    }

    private List<SubmissionsResDto> GetSubmissionsResByTask(int taskId, int multipleChoiceQuestionCount)
    {
        var submissionList = _submissionRepo.GetSubmissionListByTask(taskId);
        var submissionListRes = new List<SubmissionsResDto>();
        foreach (var submission in submissionList)
        {
            var submissionQuestionList = _submissionDetailQuestionRepo.GetStudentSubmissionDetailQuestionByTask(submission.TaskId, submission.StudentId);
            var correctOptionCount = 0;
            var submissionQuestionListRes = submissionQuestionList
                                        .Select(sq =>
                                        {
                                            if (sq.ChoiceOption != null && sq.ChoiceOption.IsCorrect) correctOptionCount++;

                                            var submissionQuestionRes = new SubmissionDetailQuestionsResDto()
                                            {
                                                QuestionId = sq.QuestionId,
                                                ChoiceOptionId = sq.ChoiceOptionId,
                                                EssayAnswerContent = sq.EssayAnswerContent,
                                            };

                                            return submissionQuestionRes;
                                        })
                                        .ToList();

            var submissionFileList = _submissionDetailFileRepo.GetStudentSubmissionDetailFileByTask(submission.TaskId, submission.StudentId);
            var submissionFileListRes = submissionFileList
                            .Select(sf =>
                            {
                                var taskFileId = sf.TaskFileId;
                                var fileIdListRes = submissionFileList
                                                .Where(sf => sf.TaskFileId == taskFileId)
                                                .Select(sf => sf.FileId)
                                                .ToList();

                                var submissionFileRes = new SubmissionDetailFilesResDto()
                                {
                                    TaskFileId = sf.TaskFileId,
                                    FileIdList = fileIdListRes,
                                };

                                return submissionFileRes;
                            })
                            .ToList();

            var multipleChoiceScore = (double)correctOptionCount / multipleChoiceQuestionCount * 100.0d;
            var submissionRes = new SubmissionsResDto()
            {
                Id = submission.Id,
                StudentFullName = submission.Student.FullName,
                FinalScore = submission.Grade,
                MultipleChoiceScore = multipleChoiceScore,
                TeacherNotes = submission.TeacherNotes,
                CreatedAt = submission.CreatedAt.ToString(IsoDateTimeFormat),
                SubmissionDetailQuestionList = submissionQuestionListRes,
                SubmissionDetailFileList = submissionFileListRes,
            };

            submissionListRes.Add(submissionRes);
        }

        return submissionListRes;
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
