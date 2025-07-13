using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using StudentProgressTracker.Shared.Ids;

namespace StudentProgressTracker.Shared.CurrentUser;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public UserId GetUserId()
    {
        var user = httpContextAccessor.HttpContext.User;

        var userId =
            user.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? user.FindFirst("sub")?.Value
            ?? UserId.Empty.Value;

        return UserId.Parse(userId);
    }

    public string GetUserName()
    {
        var user = httpContextAccessor.HttpContext.User;

        return user.Identity?.Name ?? user.FindFirst(ClaimTypes.GivenName)?.Value ?? string.Empty;
    }
}
