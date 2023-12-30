namespace LmsApi.IService;

using LmsApi.Model;

public interface IClassService
{
    List<Class> GetAllClassList();
    List<Class> GetEnrolledClassList();
    List<Class> GetClassListByTeacher();
    List<Class> GetUnEnrolledClassList();
    StudentClass EnrollClass(int classId);
    Class CreateNewClass(Class newClass);
}
