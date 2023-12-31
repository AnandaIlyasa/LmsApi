using LmsApi.Config;
using LmsApi.IRepo;
using LmsApi.Model;
using Microsoft.EntityFrameworkCore;

namespace LmsApi.Repo;

public class UserRepo : IUserRepo
{
    readonly DBContextConfig _context;

    public UserRepo(DBContextConfig context)
    {
        _context = context;
    }
    public User GetUserById(int id)
    {
        var user = _context.Users.Single(u => u.Id == id);
        return user;
    }

    public User? GetUserByEmail(string email)
    {
        var user = _context.Users
                .Where(u => u.Email == email && u.IsActive == true)
                .Include(u => u.Role)
                .FirstOrDefault();
        return user;
    }

    public User CreateNewUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }

    public List<User> GetUserListByRole(string roleCode)
    {
        var userList = _context.Users
                    .Include(u => u.Role)
                    .Where(u => u.Role.RoleCode == roleCode)
                    .ToList();
        return userList;
    }

    public int UpdateUser(User user)
    {
        var foundUser = _context.Users
                        .Where(u => u.Id == user.Id)
                        .First();
        foundUser.Pass = user.Pass;
        return _context.SaveChanges();
    }
}
