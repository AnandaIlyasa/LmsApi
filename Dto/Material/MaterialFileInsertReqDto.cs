using LmsApi.Dto.File;
using System.ComponentModel.DataAnnotations;

namespace LmsApi.Dto.Material;

public class MaterialFileInsertReqDto
{
    [MaxLength(30)]
    public string? FileName { get; set; }
    public FileDto File { get; set; }
}
