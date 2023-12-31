namespace LmsApi.Dto.User;

public class UserLoginResDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string RoleCode { get; set; }
    public string Token { get; set; }
    public int? PhotoId { get; set; }
}
