using LmsApi.Dto.File;
using System.ComponentModel.DataAnnotations;

namespace LmsApi.Dto.Material;

public class MaterialInsertReqDto
{
    [Required(ErrorMessage = "materialName is required"), MaxLength(30)]
    public string MaterialName { get; set; }
    public string MaterialDescription { get; set; }
    public List<MaterialFileInsertReqDto> MaterialFileList { get; set; }
}
