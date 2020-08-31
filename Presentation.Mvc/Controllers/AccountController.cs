using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.ViewModels.Account;
using Domain.Models;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentation.Mvc.Extensions;
using Presentation.Mvc.Models;
using System.Text.Json;

namespace Presentation.Mvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IEmailSender _emailSender;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _signInManager = signInManager;
            _userManager = userManager;

            _emailSender = emailSender;

        }

        [HttpGet]
        public IActionResult SignUp(string returnUrl = null)
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");

            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUpViewModel model, string returnUrl = null)
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email
                };


                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {

                    var emailComfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var emailMessage = Url.Action("ComfirmEmail", "Account", new { userName = user.UserName, token = emailComfirmationToken }, Request.Scheme);
                    var body = await this.RenderViewAsync("~/Views/EmailTemplates/EmailComfirmationTemplate.cshtml", emailMessage);

                    var emailResult = await _emailSender.SendEmailAsync(EmailSetting.TestEmail, "Email Comfirmation", body, user.Email, true);


                    return RedirectToAction("Wellcome", "Account", new { returnUrl = returnUrl });

                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }
            ViewData["returnUrl"] = returnUrl;

            return View(model);
        }

        [HttpGet]
        public IActionResult Wellcome(string returnUrl = null)
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");

            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ComfirmEmailAsync(string userName, string token)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(token))
                return NotFound();

            var user = await _userManager.FindByNameAsync(userName);

            if (user == null) return NotFound();

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                
                TempData["SweetAlert"] =JsonConvert.SerializeObject( new SweetAlert()
                {
                    Title = "تایید ایمیل",
                    Text = "ایمیل شما با موفقیت تایید شد",
                    Icon=SweetAlertIcon.success,
                    ShowCloseButton=false,
                    CancelButtonText="",
                    ComfirmButtonText="حله",
                    ShowCancelButton=false
                });
            }
            else
            {
                string error = "تایید ایمیل با خطا مواجه شد:";

                foreach (var e in result.Errors)
                {
                    error +=$"\n {e.Description}.";
                }

                TempData["SweetAlert"] =JsonConvert.SerializeObject( new SweetAlert()
                {
                    Title = "تایید ایمیل",
                    Text = error,
                    Icon = SweetAlertIcon.error,
                    ShowCloseButton = false,
                    CancelButtonText = "",
                    ComfirmButtonText = "حله",
                    ShowCancelButton = false
                });
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");

            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserNameOrEmail);
                if (user == null)
                    user = await _userManager.FindByEmailAsync(model.UserNameOrEmail);

                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RemmberMe, true);

                    if (result.Succeeded)
                    {
                        if (returnUrl != null && Url.IsLocalUrl(returnUrl))
                            return Redirect(returnUrl);

                        return RedirectToAction("Index", "Home");
                    }

                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError("Account Locked", "حساب شما قفل شده است");
                    }
                }

                ModelState.AddModelError("Login failed", "نام کاربری یا رمز عبور صحیح نمیباشد");


            }
            ViewData["returnUrl"] = returnUrl;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Json(true);
            }

            return Json("ایمیل انتخاب شده از قبل موجود است");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IsUserNameInUse(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return Json(true);
            }

            return Json("نام کاربری انتخاب شده از قبل موجود است");
        }
    }
}
