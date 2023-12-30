using LmsApi.Config;
using LmsApi.Model;

namespace LmsApi.IRepo;

public interface IClassRepo
{
    List<Class> GetClassList();
    List<Class> GetClassListByStudent(int studentId);
    List<Class> GetClassListByTeacher(int teacherId);
    List<Class> GetUnEnrolledClassListByStudent(int studentId);
    Class CreateNewClass(Class newClass);
}
