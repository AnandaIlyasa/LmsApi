using LmsApi.IService;
using LmsApi.Model;
using LmsApi.IRepo;

using LmsApi.Config;
using LmsApi.Constant;

namespace LmsApi.Service;

public class UserService : IUserService
{
    readonly IUserRepo _userRepo;
    readonly IRoleRepo _roleRepo;
    readonly IPrincipleService _principleService;

    public UserService(IUserRepo userRepo, IRoleRepo roleRepo, IPrincipleService principleService)
    {
        _userRepo = userRepo;
        _principleService = principleService;
        _roleRepo = roleRepo;
    }

    public User CreateNewStudent(User user)
    {
        var role = _roleRepo.GetRoleByCode(RoleCode.Student);
        user.RoleId = role.Id;

        var systemUser = _userRepo.GetUserByRole(RoleCode.System);
        user.CreatedBy = systemUser.Id;
        user.CreatedAt = DateTime.Now;
        user = _userRepo.CreateNewUser(user);
        return user;
    }

    public User CreateNewTeacher(User user)
    {
        var role = _roleRepo.GetRoleByCode(RoleCode.Teacher);
        user.RoleId = role.Id;

        user.CreatedBy = _principleService.GetLoginId();
        user.CreatedAt = DateTime.Now;
        user.Pass = Utils.Utils.GenerateRandomAlphaNumericUtil();
        user = _userRepo.CreateNewUser(user);
        return user;
    }

    public List<User> GetTeacherList()
    {
        var teacherList = _userRepo.GetUserListByRole(RoleCode.Teacher);
        return teacherList;
    }

    public User? Login(string email, string password)
    {
        var user = _userRepo.GetUserByEmailAndPassword(email, password);
        return user;
    }
}
