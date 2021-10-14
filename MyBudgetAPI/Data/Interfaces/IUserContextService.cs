using System.Security.Claims;

namespace MyBudgetAPI.Data.Interfaces
{
    public interface IUserContextService
    {
        ClaimsPrincipal User { get; }

        int GetUserId { get; }
    }
}
