using LmsApi.Dto;
using LmsApi.Dto.Class;
using LmsApi.IService;
using Microsoft.AspNetCore.Mvc;

namespace LmsApi.Controllers;

[ApiController]
[Route("classes")]
public class ClassController : ControllerBase
{
    readonly IClassService _classService;

    public ClassController(IClassService classService)
    {
        _classService = classService;
    }

    [HttpPost]
    public InsertResDto CreateClass(ClassInsertReqDto req)
    {
        var response = _classService.CreateNewClass(req);
        return response;
    }

    [HttpGet]
    public List<ClassesResDto> GetAllClassList()
    {
        var response = _classService.GetAllClassList();
        return response;
    }

    [HttpGet("teachers")]
    public List<ClassesResDto> GetClassListByTeacher()
    {
        var response = _classService.GetClassListByTeacher();
        return response;
    }

    [HttpGet("enrolled")]
    public List<ClassesResDto> GetEnrolledClassListByStudent()
    {
        var response = _classService.GetEnrolledClassList();
        return response;
    }

    [HttpGet("unenrolled")]
    public List<ClassesResDto> GetUnEnrolledClassListByStudent()
    {
        var response = _classService.GetUnEnrolledClassList();
        return response;
    }

    [HttpPost("{classId}")]
    public InsertResDto EnrollClass(int classId)
    {
        var response = _classService.EnrollClass(classId);
        return response;
    }

    [HttpPost("{classId}/learnings")]
    public InsertResDto CreateLearning(int classId, LearningInsertReqDto req)
    {
        var response = _classService.CreateClassLearning(classId, req);
        return response;
    }
}
