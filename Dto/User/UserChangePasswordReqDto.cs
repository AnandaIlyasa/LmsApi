using System.ComponentModel.DataAnnotations;

namespace LmsApi.Dto.User;

public class UserChangePasswordReqDto
{
    [Required(ErrorMessage = "OldPassword is required")]
    public string OldPassword { get; set; }

    [Required(ErrorMessage = "NewPassword is required")]
    public string NewPassword { get; set; }
}
