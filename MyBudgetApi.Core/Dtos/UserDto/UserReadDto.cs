using System;

namespace MyBudgetApi.Data.Dtos
{
    public class UserReadDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
