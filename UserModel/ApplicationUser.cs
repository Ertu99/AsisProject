﻿using Microsoft.AspNetCore.Identity;

namespace AsisProject.UserModel
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }

    }
}
