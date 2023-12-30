namespace LmsApi.IRepo;

using LmsApi.Config;
using LmsApi.Model;

public interface IUserRepo
{
    User? GetUserByEmailAndPassword(string email, string password);
    User CreateNewUser(User user);
    User GetUserByRole(string roleCode);
    List<User> GetUserListByRole(string roleCode);
}
