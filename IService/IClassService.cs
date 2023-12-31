namespace LmsApi.IService;

using LmsApi.Dto;
using LmsApi.Dto.Class;
using LmsApi.Model;

public interface IClassService
{
    List<ClassesResDto> GetAllClassList();
    List<ClassesResDto> GetEnrolledClassList();
    List<ClassesResDto> GetClassListByTeacher();
    List<Class> GetUnEnrolledClassList();
    StudentClass EnrollClass(int classId);
    InsertResDto CreateNewClass(ClassInsertReqDto req);
}
