using LmsApi.IService;
using LmsApi.Model;
using LmsApi.IRepo;

using LmsApi.Config;
using LmsApi.Dto;
using LmsApi.Dto.User;
using LmsApi.Constant;

namespace LmsApi.Service;

public class UserService : IUserService
{
    readonly IUserRepo _userRepo;
    readonly IRoleRepo _roleRepo;
    readonly IEmailService _emailService;
    readonly IPrincipleService _principleService;

    public UserService
     (
        IUserRepo userRepo,
        IRoleRepo roleRepo,
        IEmailService emailService,
        IPrincipleService principleService
    )
    {
        _userRepo = userRepo;
        _emailService = emailService;
        _roleRepo = roleRepo;
        _principleService = principleService;
    }

    public UserLoginResDto? Login(UserLoginReqDto req)
    {
        User? user = null;
        using (var context = new DBContextConfig())
        {
            user = _userRepo.GetUserByEmail(req.Email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("User not found");
            }
            var isPasswordMatch = BCrypt.Net.BCrypt.Verify(req.Password, user.Pass);
            if (isPasswordMatch == false)
            {
                throw new UnauthorizedAccessException("Password is invalid");
            }
        }

        UserLoginResDto? loggedInUser = null;
        if (user != null)
        {
            var token = Utils.JwtUtil.GenerateToken(user.Id.ToString());
            loggedInUser = new UserLoginResDto()
            {
                Id = user.Id,
                FullName = user.FullName,
                RoleCode = user.Role.RoleCode,
                Token = token,
            };
        }

        return loggedInUser;
    }

    public InsertResDto CreateTeacher(TeacherInsertReqDto req)
    {
        var user = new User()
        {
            FullName = req.FullName,
            Email = req.Email,
            CreatedBy = _principleService.GetLoginId(),
        };

        var originalPassword = Utils.Utils.GenerateRandomAlphaNumericUtil();
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(originalPassword, 12);
        user.Pass = hashedPassword;

        using (var context = new DBContextConfig())
        {
            var role = _roleRepo.GetRoleByCode(RoleCode.Teacher);

            user.RoleId = role.Id;
            user = _userRepo.CreateNewUser(user);

            _emailService.SendEmail(req.Email, "LMS: New Teacher Has Been Created", $@"
                <h3>Fullname: {user.FullName}</h3>
                <h3>Password: {originalPassword}</h3>
            ");
        }

        var insertRes = new InsertResDto()
        {
            Id = user.Id,
            Message = "Teacher successfully created",
        };

        return insertRes;
    }

    public InsertResDto CreateStudent(StudentInsertReqDto req)
    {
        var systemId = _userRepo.GetUserListByRole(RoleCode.System).First().Id;
        var user = new User()
        {
            FullName = req.FullName,
            Email = req.Email,
            CreatedBy = systemId,
            CreatedAt = DateTime.Now,
        };

        var originalPassword = req.Password;
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(originalPassword, 12);
        user.Pass = hashedPassword;

        using (var context = new DBContextConfig())
        {
            var role = _roleRepo.GetRoleByCode(RoleCode.Student);

            user.RoleId = role.Id;
            user = _userRepo.CreateNewUser(user);

            _emailService.SendEmail(req.Email, "LMS: New Student Has Been Created", $@"
                <h3>Fullname: {user.FullName}</h3>
                <h3>Password: {originalPassword}</h3>
            ");
        }

        var insertRes = new InsertResDto()
        {
            Id = user.Id,
            Message = "Student successfully created",
        };

        return insertRes;
    }

    public List<UsersResDto> GetTeacherList()
    {
        var userList = new List<User>();
        using (var context = new DBContextConfig())
        {
            userList = _userRepo.GetUserListByRole(RoleCode.Teacher);
        }
        var userResList = userList
                          .Select(u =>
                            new UsersResDto()
                            {
                                Id = u.Id,
                                FullName = u.FullName,
                            }
                          )
                          .ToList();
        return userResList;
    }

    public UpdateResDto ChangePassword(UserChangePasswordReqDto req)
    {
        var userId = _principleService.GetLoginId();
        var user = _userRepo.GetUserById(userId);

        var isPasswordMatch = BCrypt.Net.BCrypt.Verify(req.OldPassword, user.Pass);
        if (isPasswordMatch == false)
        {
            throw new Exception("Old password is not matched");
        }

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(req.NewPassword, 13);
        user.Pass = hashedPassword;
        user.UpdatedBy = _principleService.GetLoginId();
        user.UpdatedAt = DateTime.Now;
        var rowsAffected = _userRepo.UpdateUser(user);

        _emailService.SendEmail(user.Email, "LMS: Password Has Been Changed", "<h3>Password has been changed</h3>");

        var response = new UpdateResDto()
        {
            Version = rowsAffected.ToString(),
            Message = "Password successfully changed",
        };

        return response;
    }
}
