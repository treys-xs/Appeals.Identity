using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Appeals.Identity.Models;
using Appeals.Identity.ViewModels;

namespace Appeals.Identity.Controllers
{

    [Route("[controller]/[action]")]
    public class AuthController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AuthController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager) =>
            (_signInManager, _userManager) = (signInManager, userManager);

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userManager.FindByNameAsync(viewModel.Username);

            if(user == null)
            {
                return View(viewModel);
            }

            var result = await 
                _signInManager.PasswordSignInAsync(user, viewModel.Password, false, false);

            if (result.Succeeded)
            {
                return Redirect(viewModel.ReturnUrl);
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = new AppUser
            {
                UserName = viewModel.Username
            };

            var result = await _userManager.CreateAsync(user, viewModel.Password);
            if (result.Succeeded)
            {
                return Ok();
            }

            return View(viewModel);
        }
    }
}
