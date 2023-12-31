using LmsApi.Config;
using LmsApi.Dto;
using LmsApi.Dto.Class;
using LmsApi.IRepo;
using LmsApi.IService;
using LmsApi.Model;

namespace LmsApi.Service;

public class ClassService : IClassService
{
    readonly IClassRepo _classRepo;
    readonly IStudentClassRepo _studentClassRepo;
    readonly ILearningRepo _learningRepo;
    readonly ISessionRepo _sessionRepo;
    readonly ILMSFileRepo _fileRepo;
    readonly IPrincipleService _principleService;

    public ClassService
    (
        IClassRepo classRepo,
        IStudentClassRepo studentClassRepo,
        ILearningRepo learningRepo,
        ISessionRepo sessionRepo,
        ILMSFileRepo fileRepo,
        IPrincipleService principleService
    )
    {
        _classRepo = classRepo;
        _studentClassRepo = studentClassRepo;
        _learningRepo = learningRepo;
        _sessionRepo = sessionRepo;
        _fileRepo = fileRepo;
        _principleService = principleService;
    }

    public List<ClassesResDto> GetEnrolledClassList()
    {
        var enrolledClassList = _classRepo.GetClassListByStudent(_principleService.GetLoginId());
        var response = enrolledClassList
                    .Select(c =>
                    {
                        var learningListRes = GetClassLearningsSessionsRes(c);
                        var classRes = new ClassesResDto()
                        {
                            ClassCode = c.ClassCode,
                            ClassName = c.ClassTitle,
                            ClassDescription = c.ClassDescription,
                            ClassImageId = c.ClassImageId,
                            LearningList = learningListRes,
                        };
                        return classRes;
                    })
                    .ToList();
        return response;
    }

    public List<Class> GetUnEnrolledClassList()
    {
        var unEnrolledClassList = _classRepo.GetUnEnrolledClassListByStudent(_principleService.GetLoginId());
        return unEnrolledClassList;
    }

    public StudentClass EnrollClass(int classId)
    {
        var studentId = _principleService.GetLoginId();
        var studentClass = new StudentClass()
        {
            StudentId = studentId,
            ClassId = classId,
        };
        studentClass = _studentClassRepo.CreateNewStudentClass(studentClass);
        return studentClass;
    }

    public List<ClassesResDto> GetClassListByTeacher()
    {
        var classList = _classRepo.GetClassListByTeacher(_principleService.GetLoginId());
        var response = classList
                    .Select(c =>
                    {
                        var learningListRes = GetClassLearningsSessionsRes(c);
                        var classRes = new ClassesResDto()
                        {
                            ClassCode = c.ClassCode,
                            ClassName = c.ClassTitle,
                            ClassDescription = c.ClassDescription,
                            ClassImageId = c.ClassImageId,
                            LearningList = learningListRes,
                        };
                        return classRes;
                    })
                    .ToList();
        return response;
    }

    List<LearningsResDto> GetClassLearningsSessionsRes(Class cls)
    {
        const string isoDateFormat = "yyyy-MM-dd";
        const string isoTimeFormat = "HH:mm";
        var learningList = _learningRepo.GetLearningListByClass(cls.Id);
        var learningListRes = learningList
                                .Select(l =>
                                {
                                    var sessionList = _sessionRepo.GetSessionListByLearning(l.Id);
                                    var sessionListRes = sessionList
                                                            .Select(s =>
                                                                new SessionsResDto()
                                                                {
                                                                    Id = s.Id,
                                                                    SessionName = s.SessionName,
                                                                    SessionDescription = s.SessionDescription,
                                                                    StartTime = s.StartTime.ToString(isoTimeFormat),
                                                                    EndTime = s.EndTime.ToString(isoTimeFormat),
                                                                }
                                                            )
                                                            .ToList();
                                    var learningRes = new LearningsResDto()
                                    {
                                        LearningName = l.LearningName,
                                        LearningDescription = l.LearningDescription,
                                        LearningDate = l.LearningDate.ToString(isoDateFormat),
                                        SessionList = sessionListRes,
                                    };
                                    return learningRes;
                                })
                                .ToList();

        return learningListRes;
    }

    public InsertResDto CreateNewClass(ClassInsertReqDto req)
    {
        InsertResDto response;
        using (var context = new DBContextConfig())
        {
            var trx = context.Database.BeginTransaction();

            var classImage = new LMSFile()
            {
                FileContent = req.ClassImage.FileContent,
                FileExtension = req.ClassImage.FileExtension,
                CreatedAt = DateTime.Now,
                CreatedBy = _principleService.GetLoginId(),
            };

            var insertedFile = _fileRepo.CreateNewFile(classImage);

            var newClass = new Class()
            {
                ClassCode = req.ClassCode,
                ClassTitle = req.ClassName,
                ClassImageId = classImage.Id,
                ClassDescription = req.ClassDescription,
                TeacherId = req.TeacherId,
                CreatedAt = DateTime.Now,
                CreatedBy = _principleService.GetLoginId(),
            };
            var insertedClass = _classRepo.CreateNewClass(newClass);

            response = new InsertResDto()
            {
                Id = insertedClass.Id,
                Message = "Class successfully created",
            };

            trx.Commit();
        }
        return response;
    }

    public List<ClassesResDto> GetAllClassList()
    {
        var classList = _classRepo.GetClassList();
        var response = classList
                    .Select(c =>
                        new ClassesResDto()
                        {
                            ClassCode = c.ClassCode,
                            ClassName = c.ClassTitle,
                            TeacherFullName = c.Teacher.FullName,
                        }
                    )
                    .ToList();
        return response;
    }
}
