using Microsoft.AspNetCore.Identity;

namespace Game2v.Security
{
    public class  AppIdentityRole : IdentityRole
    {
        public string Description { get; set; }
    }
}