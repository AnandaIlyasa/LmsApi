using LmsApi.Config;
using LmsApi.Model;

namespace LmsApi.IRepo;

public interface IStudentClassRepo
{
    StudentClass CreateNewStudentClass(StudentClass studentClass);
}
