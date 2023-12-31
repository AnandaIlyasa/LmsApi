namespace LmsApi.Controllers;

using LmsApi.Dto;
using LmsApi.Dto.User;
using LmsApi.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login"), AllowAnonymous]
    public UserLoginResDto? Login(UserLoginReqDto req)
    {
        var response = _userService.Login(req);
        return response;
    }

    [HttpPost("teachers")]
    public InsertResDto InsertTeacher([FromBody] TeacherInsertReqDto req)
    {
        var response = _userService.CreateTeacher(req);
        return response;
    }

    [HttpGet("teachers")]
    public List<UsersResDto> GetTeacherList()
    {
        var response = _userService.GetTeacherList();
        return response;
    }

    [HttpPost("students")]
    public InsertResDto InsertStudent([FromBody] StudentInsertReqDto req)
    {
        var response = _userService.CreateStudent(req);
        return response;
    }

    [HttpPatch("password")]
    public UpdateResDto ChangePassword([FromBody] UserChangePasswordReqDto req)
    {
        var response = _userService.ChangePassword(req);
        return response;
    }
}
