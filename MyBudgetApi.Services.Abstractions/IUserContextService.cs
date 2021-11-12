using System.Security.Claims;

namespace MyBudgetApi.Services.Abstractions
{
    public interface IUserContextService
    {
        ClaimsPrincipal User { get; }

        int GetUserId { get; }
    }
}
