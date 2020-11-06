using Game2v.Model;
using Game2v.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Game2v.Classes.Control
{
    [Route("/[controller]/[action]/")]
    [Controller]
    public class SecurityController : Controller
        {
            private readonly UserManager<AppIdentityUser> userManager;
            private readonly RoleManager<AppIdentityRole> roleManager;
            private readonly SignInManager<AppIdentityUser> signinManager;
            public SecurityController(UserManager<AppIdentityUser> userManager,
                RoleManager<AppIdentityRole> roleManager,
                SignInManager<AppIdentityUser> signinManager)
            {
                this.userManager = userManager;
                this.roleManager = roleManager;
                this.signinManager = signinManager;
            }
            [HttpGet]
            public IActionResult Register()
            {
                return View();
            }
            [HttpPost]
            public IActionResult Register(Register obj)
            {
                if (ModelState.IsValid)
                {
                    if (!roleManager.RoleExistsAsync("Manager").Result)
                    {
                        AppIdentityRole role = new AppIdentityRole();
                        role.Name = "Manager";
                        role.Description = "Pode realizar operações de cadastro";
                        IdentityResult roleResult =
                        roleManager.CreateAsync(role).Result;
                    }
                    AppIdentityUser user = new AppIdentityUser();
                    user.UserName = obj.UserName;
                    user.Email = obj.Email;
                    user.FullName = obj.FullName;
                    user.BirthDate = obj.BirthDate;

                    IdentityResult result = userManager.CreateAsync(user, obj.Password).Result;
                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "Manager").Wait();
                        return RedirectToAction("SignIn", "Security");
                    }
                    else
                    {
                        System.Console.WriteLine(result.ToString());
                        ViewBag.Message ="Dados incompletos ou inválidos. A senha deve ter 6 caracteres incluindo letras minúsculas e números.";
                    }
                }
                return View(obj);
            }

            [HttpGet]
            public IActionResult SignIn()
            {
                return View();
            }

            [HttpPost]
            public IActionResult SignIn(SignIn obj)
            {
                if (ModelState.IsValid)
                {
                    var result = signinManager.PasswordSignInAsync
                    (obj.UserName, obj.Password,
                    obj.RememberMe, false).Result;

                    if (result.Succeeded)
                    {
                        return RedirectToAction("List", "Game");
                    }
                        else
                    {
                        ViewBag.Message = "Falha no login!";
                    }
                }
                return View(obj);
            }

            [HttpGet]
            public IActionResult SignOut()
            {
                signinManager.SignOutAsync().Wait();
                return RedirectToAction("SignIn", "Security");
            }
        }
}