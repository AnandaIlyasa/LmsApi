using LmsApi.Dto;
using LmsApi.Dto.User;
using LmsApi.Model;

namespace LmsApi.IService;

public interface IUserService
{
    UserLoginResDto? Login(UserLoginReqDto req);
    InsertResDto CreateTeacher(TeacherInsertReqDto req);
    InsertResDto CreateStudent(StudentInsertReqDto req);
    UpdateResDto ChangePassword(UserChangePasswordReqDto req);
    List<UsersResDto> GetTeacherList();
}
