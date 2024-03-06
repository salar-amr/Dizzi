using System.Security.Claims;

using Application.Common.Interfaces;

namespace Web.Services;

public class CurrentUser : IUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int Id => Convert.ToInt32(_httpContextAccessor.HttpContext?.Items["UserId"]);
}
