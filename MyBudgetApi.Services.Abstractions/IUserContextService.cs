using System.Security.Claims;

namespace MyBudgetApi.Data.Abstractions
{
    public interface IUserContextService
    {
        ClaimsPrincipal User { get; }

        int GetUserId { get; }
    }
}
