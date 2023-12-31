namespace LmsApi.Dto.Class;

public class ClassesResDto
{
    public int? Id { get; set; }
    public string ClassCode { get; set; }
    public string ClassName { get; set; }
    public string? TeacherFullName { get; set; }
    public string? ClassDescription { get; set; }
    public int? ClassImageId { get; set; }
    public List<LearningsResDto>? LearningList { get; set; }
}
