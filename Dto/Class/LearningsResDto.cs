namespace LmsApi.Dto.Class;

public class LearningsResDto
{
    public string LearningName { get; set; }
    public string LearningDescription { get; set; }
    public string LearningDate { get; set; }
    public List<SessionsResDto> SessionList { get; set; }
}
