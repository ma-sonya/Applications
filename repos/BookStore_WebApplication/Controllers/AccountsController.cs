using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BookStore_WebApplication.ViewModels;
using BookStore_WebApplication.Models;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;

namespace BookStore_WebApplication.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountsController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email, Year = model.Year };

                //Add an user
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //Installing coocie
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action(
                        "Login",
                        "Accounts",
                        new {userId = user.Id, code = code},
                        protocol: HttpContext.Request.Scheme);  
                    EmailService emailService = new EmailService();
                    await emailService.SendEmailAsync(model.Email, "Confirm your account",
                        $"Для підтвердження реєстрації перейдіть за посиланням: <a href='{callbackUrl}'>link</a>");

                    await _signInManager.SignInAsync(user, false);
                    var userRoles = await _userManager.GetRolesAsync(user);
                    if (userRoles != null)
                        userRoles.Add("user");
                    await _userManager.AddToRolesAsync(user, userRoles);

                    return Content("Для завершення реєстрації, перевірте електронну пошту та перейдіть за вказаним посиланням. \n За потреби, перевірте спам.");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);  
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
                return View("Error");

        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {                
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Невірний логін або пароль");      
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            //delete autentification coocie
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
