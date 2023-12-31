namespace LmsApi.IRepo;

using LmsApi.Config;
using LmsApi.Model;

public interface IUserRepo
{
    User GetUserById(int id);
    User? GetUserByEmail(string email);
    User CreateNewUser(User user);
    int UpdateUser(User user);
    List<User> GetUserListByRole(string roleCode);
}
