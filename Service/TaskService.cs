using LmsApi.Config;
using LmsApi.Constant;
using LmsApi.Dto;
using LmsApi.Dto.Session;
using LmsApi.Dto.Task;
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
                                    .GroupBy(sf => sf.TaskFileId)
                                    .ToList()
                                    .Select(group =>
                                    {
                                        var fileIdListRes = group
                                                        .Select(sf => sf.FileId)
                                                        .ToList();

                                        var submissionFileRes = new SubmissionDetailFilesResDto()
                                        {
                                            TaskFileId = group.Key,
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

    public TaskDetailsStudentSubmissionResDto GetTaskDetailAndStudentSubmissionByTask(int taskId)
    {
        var studentId = _principleService.GetLoginId();
        var submission = _submissionRepo.GetStudentSubmissionByTask(studentId, taskId);

        var taskQuestionListRes = GetQuestionsRes(taskId);
        var multipleChoiceQuestionCount = 0;
        foreach (var question in taskQuestionListRes)
            if (question.QuestionType == QuestionType.MultipleChoice) multipleChoiceQuestionCount++;

        var taskFileListRes = GetTaskFilesRes(taskId);

        List<SubmissionDetailFilesResDto>? submissionFileListRes = null;
        List<SubmissionDetailQuestionsResDto>? submissionQuestionListRes = null;
        var correctOptionCount = 0;
        if (submission != null)
        {
            var submissionQuestionList = _submissionDetailQuestionRepo.GetStudentSubmissionDetailQuestionByTask(taskId, studentId);
            submissionQuestionListRes = submissionQuestionList
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


            var submissionFileList = _submissionDetailFileRepo.GetStudentSubmissionDetailFileByTask(taskId, studentId);
            submissionFileListRes = submissionFileList
                                    .GroupBy(sf => sf.TaskFileId)
                                    .ToList()
                                    .Select(group =>
                                    {
                                        var fileIdListRes = group
                                                        .Select(sf => sf.FileId)
                                                        .ToList();

                                        var submissionFileRes = new SubmissionDetailFilesResDto()
                                        {
                                            TaskFileId = group.Key,
                                            FileIdList = fileIdListRes,
                                        };

                                        return submissionFileRes;
                                    })
                                    .ToList();
        }

        var multipleChoiceScore = (double)correctOptionCount / multipleChoiceQuestionCount * 100.0d;

        var response = new TaskDetailsStudentSubmissionResDto()
        {
            TaskFileList = taskFileListRes,
            TaskQuestionList = taskQuestionListRes,
            Submission = submission == null ? null : new SubmissionsResDto()
            {
                FinalScore = submission.Grade,
                TeacherNotes = submission.TeacherNotes,
                MultipleChoiceScore = multipleChoiceScore,
                SubmissionDetailFileList = submissionFileListRes,
                SubmissionDetailQuestionList = submissionQuestionListRes,
                CreatedAt = submission.CreatedAt.ToString(IsoDateTimeFormat),
            },
        };
        return response;
    }

    public UpdateResDto InsertScoreAndNotes(int submissionId, TaskSubmissionScoreAndNotesReqDto req)
    {
        var submission = _submissionRepo.GetSubmissionById(submissionId);

        var questionList = _questionRepo.GetQuestionListByTask(submission.TaskId);
        var multipleChoiceQuestionCount = 0;
        foreach (var question in questionList)
            if (question.QuestionType == QuestionType.MultipleChoice) multipleChoiceQuestionCount++;

        var submissionQuestionList = _submissionDetailQuestionRepo.GetStudentSubmissionDetailQuestionByTask(submission.TaskId, submission.StudentId);
        var correctOptionCount = 0;
        submissionQuestionList.ForEach(sq =>
        {
            if (sq.ChoiceOption != null && sq.ChoiceOption.IsCorrect) correctOptionCount++;
        });

        var multipleChoiceScore = (double)correctOptionCount / multipleChoiceQuestionCount * 100.0d;

        submission.Grade = (multipleChoiceScore + req.Grade) / 2.0d;
        submission.TeacherNotes = req.TeacherNotes;
        submission.UpdatedAt = DateTime.Now;
        submission.UpdatedBy = _principleService.GetLoginId();
        var affectedRows = _submissionRepo.UpdateSubmissionGradeAndNotes(submission);

        var response = new UpdateResDto()
        {
            Version = affectedRows.ToString(),
            Message = "Score and notes successfully inserted",
        };
        return response;
    }

    public InsertResDto SubmitTask(int taskId, TaskSubmissionDetailsReqDto req)
    {
        InsertResDto response;
        using (var context = new DBContextConfig())
        {
            using (var trx = context.Database.BeginTransaction())
            {
                try
                {
                    var studentId = _principleService.GetLoginId();

                    var submission = new Submission()
                    {
                        StudentId = _principleService.GetLoginId(),
                        TaskId = taskId,
                        CreatedAt = DateTime.Now,
                        CreatedBy = _principleService.GetLoginId(),
                    };
                    submission = _submissionRepo.CreateNewSubmission(submission);

                    foreach (var submissionFileReq in req.SubmissionDetailFiles)
                    {
                        foreach (var fileReq in submissionFileReq.FileList)
                        {
                            var file = new LMSFile()
                            {
                                FileContent = fileReq.FileContent,
                                FileExtension = fileReq.FileExtension,
                                CreatedAt = DateTime.Now,
                                CreatedBy = studentId,
                            };
                            file = _fileRepo.CreateNewFile(file);

                            var fileSubmission = new SubmissionDetailFile()
                            {
                                SubmissionId = submission.Id,
                                TaskFileId = submissionFileReq.TaskFileId,
                                FileId = file.Id,
                                CreatedAt = DateTime.Now,
                                CreatedBy = studentId,
                            };
                            _submissionDetailFileRepo.CreateNewSubmissionDetailFile(fileSubmission);
                        }
                    }

                    foreach (var submissionQuestionReq in req.SubmissionDetailQuestions)
                    {
                        var questionSubmission = new SubmissionDetailQuestion()
                        {
                            SubmissionId = submission.Id,
                            QuestionId = submissionQuestionReq.QuestionId,
                            ChoiceOptionId = submissionQuestionReq.ChoiceOptionId,
                            EssayAnswerContent = submissionQuestionReq.EssayAnswerContent,
                            CreatedAt = DateTime.Now,
                            CreatedBy = studentId,
                        };
                        _submissionDetailQuestionRepo.CreateNewSubmissionDetailQuestion(questionSubmission);
                    }

                    response = new InsertResDto()
                    {
                        Id = submission.Id,
                        Message = "Task successfully submitted",
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
