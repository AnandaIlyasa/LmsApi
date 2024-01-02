using System.ComponentModel.DataAnnotations;

namespace LmsApi.Dto.User;

public class StudentInsertReqDto
{
    [Required(ErrorMessage = "FullName is required"), StringLength(30)]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Email is required"), StringLength(30)]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required"), MinLength(5)]
    public string? Password { get; set; }
}
