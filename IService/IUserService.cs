using LmsApi.Config;
using LmsApi.Model;

namespace LmsApi.IService;

public interface IUserService
{
    User? Login(string email, string password);
    User CreateNewStudent(User user);
    User CreateNewTeacher(User user);
    List<User> GetTeacherList();
}
