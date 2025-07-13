using StudentProgressTracker.Shared.Ids;

namespace StudentProgressTracker.Shared.CurrentUser;

public interface ICurrentUserService
{
    UserId GetUserId();

    string GetUserName();
}
