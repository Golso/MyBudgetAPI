using MyBudgetAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudgetAPI.Data.Interfaces
{
    public interface IAccountRepository
    {
        public void RegisterUser(RegisterUserDto dto);
    }
}
