using System;

namespace MyBudgetAPI.Dtos.UserDto
{
    public class UserReadDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
