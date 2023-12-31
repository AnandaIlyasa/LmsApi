namespace LmsApi.Dto.Session;

public class SessionMaterialsResDto
{
    public string MaterialName { get; set; }
    public string? MaterialDescription { get; set; }
    public List<MaterialFileResDto>? MaterialFileList { get; set; }
}
