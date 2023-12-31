namespace LmsApi.Dto.Session;

public class SessionAttendancesResDto
{
    public int Id { get; set; }
    public bool IsApproved { get; set; }
    public string StudentFullName { get; set; }
    public string CreatedAt { get; set; }
}
