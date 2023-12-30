using LmsApi.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace LmsApi.Controllers;

[ApiController]
[Route("/files")]
public class FileController : ControllerBase
{
    //IDocumentService _fileService { get; set; }

    //public FileController(IDocumentService fileService)
    //{
    //    _fileService = fileService;
    //}

    //[AllowAnonymous]
    //[HttpGet("{id}")]
    //public IActionResult GetFileById(int id)
    //{
    //    var file = _fileService.GetById(id);
    //    var contentByteArr = Convert.FromBase64String(file.FileContent);

    //    var fileProvider = new FileExtensionContentTypeProvider();
    //    if (!fileProvider.TryGetContentType(file.FileExtension, out string contentType))
    //    {
    //        throw new ArgumentOutOfRangeException($"Unable to find Content Type for {file.FileExtension}");
    //    }

    //    return File(contentByteArr, contentType, $"file{file.FileExtension}");
    //}
}
