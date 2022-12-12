using AspNetMvcClass.Models.Data;
using AspNetMvcClass.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AspNetMvcClass.Controllers;

public class UserController : Controller
{

    private readonly UserManager<IdentityUser> userManager;
    private readonly SignInManager<IdentityUser> signInManager;
    private readonly RoleManager<IdentityRole> roleManager;

    public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.roleManager = roleManager;       
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterUserViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

        }
        return await Task.Run(() => View("Register", model));
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {           
            var identityResult = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (identityResult.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Email o Password incorretta");
        }
        return await Task.Run(() => View("Login", model));
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Login");
    }
    [HttpGet]
    public IActionResult AssignRole()
    {
        return View();
    }
    //[HttpPost]
    //public async Task<IActionResult> AssignRole(string userEmail, string roleName)
    //{
    //    // Get the user and the role using the email address and role name
    //    var user = await userManager.FindByEmailAsync(userEmail);
    //    var role = await roleManager.FindByNameAsync(roleName);
    //    if (role == null)
    //    {
    //        await roleManager.CreateAsync(new IdentityRole { Name = $"{roleName}" });
    //    }

    //    // Add the user to the role
    //    var result = await userManager.AddToRoleAsync(user, roleName);

    //    // Check if the operation was successful
    //    if (result.Succeeded)
    //    {
    //        // The user was added to the role
    //    }
    //    else
    //    {
    //        // There was an error adding the user to the role
    //    }
    //    return RedirectToAction("Login");
    //    //Task.Run(async () =>
    //    //{
    //    //    await roleManager.CreateAsync(new IdentityRole { Name = "admin" });
    //    //    await AssignRole("ex@gmail.com", "admin");
    //    //});
    //}

    //private async Task MasterLogin(LoginViewModel model)
    //{
    //    if (model.Email == "****" && model.Password == "*****")
    //    {
    //        var claims = new List<Claim>()
    //        {
    //            new Claim(ClaimTypes.Email, "Admin@Libero.it"),
    //            new Claim("Master", "MasterAccount")
    //        };
    //        var identity = new ClaimsIdentity(claims, "MyCookieAuth");
    //        ClaimsPrincipal principal = new(identity);
    //        await HttpContext.SignInAsync("MyCookieAuth", principal);
    //    }
    //}
}
