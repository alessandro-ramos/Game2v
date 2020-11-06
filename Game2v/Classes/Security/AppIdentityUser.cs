using System;
using Microsoft.AspNetCore.Identity;

namespace Game2v.Security
{
    public class AppIdentityUser : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}