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
    public InsertResDto Insert(ClassInsertReqDto req)
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
}
