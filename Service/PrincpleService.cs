using LmsApi.IService;
using System.Security.Claims;

namespace LmsApi.Service;

public class PrincpleService : IPrincipleService
{
    readonly IHttpContextAccessor _httpContextAccessor;

    public PrincpleService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int GetLoginId()
    {
        var userId = (_httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity)
                    .Claims.First(sub => sub.Type == "iss").Value;
        return Convert.ToInt32(userId);
    }
}
