﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudgetAPI.Dtos.UserDto
{
    public class UserReadDto
    {
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
