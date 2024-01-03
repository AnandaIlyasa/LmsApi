namespace LmsApi.IRepo;

using LmsApi.Model;

public interface IUserRepo
{
    User GetUserById(int id);
    User? GetUserByEmail(string email);
    List<User> GetUserListByRole(string roleCode);
}
