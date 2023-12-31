using System.ComponentModel.DataAnnotations;

namespace LmsApi.Dto.User;

public class UserLoginReqDto
{
    [Required(ErrorMessage = "Email is required"), EmailAddress, StringLength(30)]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}
