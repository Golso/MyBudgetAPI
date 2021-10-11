using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyBudgetAPI.Data.Interfaces
{
    public interface IUserContextService
    {
        ClaimsPrincipal User { get; }

        int GetUserId { get; }
    }
}
