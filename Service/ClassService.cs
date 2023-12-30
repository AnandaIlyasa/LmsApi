using LmsApi.Config;
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

    public List<Class> GetEnrolledClassList()
    {
        var enrolledClassList = _classRepo.GetClassListByStudent(_principleService.GetLoginId());
        GetClassContents(enrolledClassList);
        return enrolledClassList;
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

    public List<Class> GetClassListByTeacher()
    {
        var classList = _classRepo.GetClassListByTeacher(_principleService.GetLoginId());
        GetClassContents(classList);
        return classList;
    }

    void GetClassContents(List<Class> classList)
    {
        foreach (var cls in classList)
        {
            var learningList = _learningRepo.GetLearningListByClass(cls.Id);
            cls.LearningList = learningList;
            foreach (var learning in learningList)
            {
                var sessionList = _sessionRepo.GetSessionListByLearning(learning.Id);
                learning.SessionList = sessionList;
            }
        }
    }

    public Class CreateNewClass(Class newClass)
    {
        using (var context = new DBContextConfig())
        {
            var trx = context.Database.BeginTransaction();

            newClass.ClassImage.CreatedBy = _principleService.GetLoginId();
            newClass.ClassImage.CreatedAt = DateTime.Now;
            var insertedFile = _fileRepo.CreateNewFile(newClass.ClassImage);

            newClass.ClassImageId = insertedFile.Id;
            newClass.CreatedBy = _principleService.GetLoginId();
            newClass.CreatedAt = DateTime.Now;
            _classRepo.CreateNewClass(newClass);

            trx.Commit();
        }
        return newClass;
    }

    public List<Class> GetAllClassList()
    {
        var classList = _classRepo.GetClassList();
        return classList;
    }
}
