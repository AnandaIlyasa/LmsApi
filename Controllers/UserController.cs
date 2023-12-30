namespace LmsApi.Controllers;

using LmsApi.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/users")]
public class UserController : ControllerBase
{
    readonly IUserService _userService;
    readonly IEmailService _emailService;

    public UserController(IUserService userService, IEmailService emailService)
    {
        _userService = userService;
        _emailService = emailService;
    }

    [AllowAnonymous]
    [HttpGet]
    public void SendEmail()
    {
        var receiver = "maulananinja@gmail.com";
        _emailService.SendEmail(receiver, "Ramalan cuaca hari ini", "hujan");
    }
    //[HttpGet("{roleCode}")]
    //public List<UsersResDto> GetUserListByRole(string roleCode)
    //{
    //    var response = _userService.GetUserListByRole(roleCode);
    //    return response;
    //}

    //[HttpGet("roles")]
    //public List<RolesResDto> GetReviewerHRRoleList()
    //{
    //    var response = _userService.GetReviewerAndHRRole();
    //    return response;
    //}

    //[HttpPost]
    //public InsertResDto Insert([FromBody] UserInsertReqDto req)
    //{
    //    var response = _userService.CreateUser(req);
    //    return response;
    //}

    //[HttpPatch("password")]
    //public UpdateResDto ChangePassword([FromBody] UserChangePasswordReqDto req)
    //{
    //    var response = _userService.ChangePassword(req);
    //    return response;
    //}

    //[HttpPost("login"), AllowAnonymous]
    //public UserLoginResDto? Login(UserLoginReqDto req)
    //{
    //    var response = _userService.Login(req);
    //    return response;
    //}
}
