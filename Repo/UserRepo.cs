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

    public User CreateNewUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }

    public User? GetUserByEmailAndPassword(string email, string password)
    {
        var user = _context.Users
                .Where(u => u.Email == email && EF.Functions.Like(u.Pass, password) && u.IsActive == true)
                .Include(u => u.Role)
                .FirstOrDefault();
        return user;
    }

    public User GetUserByRole(string roleCode)
    {
        var user = _context.Users
                    .Include(u => u.Role)
                    .Where(u => u.Role.RoleCode == roleCode)
                    .First();
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
}
