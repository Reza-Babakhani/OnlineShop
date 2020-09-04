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
using Microsoft.AspNetCore.Authorization;

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
        public async Task<IActionResult> ComfirmEmail(string userName, string token)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(token))
                return NotFound();

            var user = await _userManager.FindByNameAsync(userName);

            if (user == null) return NotFound();

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {

                TempData["SweetAlert"] = JsonConvert.SerializeObject(new SweetAlert()
                {
                    Title = "تایید ایمیل",
                    Text = "ایمیل شما با موفقیت تایید شد",
                    Icon = SweetAlertIcon.success,
                    ShowCloseButton = false,
                    CancelButtonText = "",
                    ComfirmButtonText = "حله",
                    ShowCancelButton = false
                });
            }
            else
            {
                string error = "تایید ایمیل با خطا مواجه شد:";

                foreach (var e in result.Errors)
                {
                    error += $"\n {e.Description}.";
                }

                TempData["SweetAlert"] = JsonConvert.SerializeObject(new SweetAlert()
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


        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var resetPassToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var emailMessage = Url.Action("ResetPassword", "Account", new { userName = user.UserName, token = resetPassToken }, Request.Scheme);
                    var body = await this.RenderViewAsync("~/Views/EmailTemplates/ResetPasswordEmailTemplate.cshtml", emailMessage);

                    var emailResult = await _emailSender.SendEmailAsync(EmailSetting.TestEmail, "Email Comfirmation", body, user.Email, true);

                    if (emailResult.IsSuccess)
                    {
                        TempData["SweetAlert"] = JsonConvert.SerializeObject(new SweetAlert()
                        {
                            Title = "عملیات موفق",
                            Text = "ایمیل تغییر رمزعبور با موفقیت ارسال شد",
                            Icon = SweetAlertIcon.success,
                            ShowCloseButton = false,
                            CancelButtonText = "",
                            ComfirmButtonText = "حله",
                            ShowCancelButton = false
                        });

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("Email doesn't send", "ارسال ایمیل تغییر رمزعبور با خطا مواجه شد");
                    }
                }
                else
                {
                    ModelState.AddModelError("User Not Found", "کاربری با این مشخصات یافت نشد");

                }


            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ResetPassword(string userName, string token)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(token))
                return NotFound();

            var model = new ResetPasswordViewModel
            {
                Token = token,
                UserName = userName
            };

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Token))
                return NotFound();

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null)
                    return NotFound();

                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

                if (result.Succeeded)
                {
                    TempData["SweetAlert"] = JsonConvert.SerializeObject(new SweetAlert()
                    {
                        Title = "تغییر رمزعبور",
                        Text = "رمزعبور شما با موفقیت تغییر کرد",
                        Icon = SweetAlertIcon.success,
                        ShowCloseButton = false,
                        CancelButtonText = "",
                        ComfirmButtonText = "حله",
                        ShowCancelButton = false
                    });

                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);

                    }
                }
            }

            return View(model);

        }

        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);

                if (result.Succeeded)
                {
                    TempData["SweetAlert"] = JsonConvert.SerializeObject(new SweetAlert()
                    {
                        Title = "تغییر رمزعبور",
                        Text = "رمزعبور شما با موفقیت تغییر کرد لطفا با رمزعبور جدید وارد شوید",
                        Icon = SweetAlertIcon.success,
                        ShowCloseButton = false,
                        CancelButtonText = "",
                        ComfirmButtonText = "حله",
                        ShowCancelButton = false
                    });

                    await _signInManager.SignOutAsync();

                    return RedirectToAction("Login", "Account");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> VerifyMobile([FromServices] ISmsSender smsSender)
        {
            var user = await _userManager.GetUserAsync(User);

            if (DateTime.Now>user.SendPhoneConfirmationSmsLimitUntil)
            {

                if (await _userManager.IsPhoneNumberConfirmedAsync(user))
                {
                    return RedirectToAction("Index", "Profile");
                }

                user.SendPhoneConfirmationSmsLimitUntil = DateTime.Now.AddMinutes(3);
                await _userManager.UpdateAsync(user);
                user = await _userManager.GetUserAsync(User);

                var token = await _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);
                string message = $"کد تایید شما در دیجی استور: {token}";

                //تغییر ارسال پیام
                smsSender.SendMessage(message, new string[] { user.PhoneNumber }, "30004747475547", SmsSetting.testSms);
                //
            }
            else
            {
                var remainingTime = user.SendPhoneConfirmationSmsLimitUntil - DateTime.Now;
                string error = "";
                error = "کد تایید به تازگی ارسال شده است\nارسال مجدد در ";

                if (remainingTime.Minutes > 0)
                {
                    error += remainingTime.Minutes + " دقیقه ";

                    if(remainingTime.Seconds>0)
                        error +="و "+ remainingTime.Seconds + " ثانیه ";

                }
                else
                {

                    if (remainingTime.Seconds > 0)
                        error +=  remainingTime.Seconds + " ثانیه ";
                }

                error += " دیگر امکان پذیر میباشد";
                ViewData["Errors"] = error;
            }
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> VerifyMobile(string d1, string d2, string d3, string d4, string d5, string d6)
        {
            string errors = "";

            string token = d1 + d2 + d3 + d4 + d5 + d6;
            if (!string.IsNullOrEmpty(token))
            {
                var user = await _userManager.GetUserAsync(User);

                var result = await _userManager.ChangePhoneNumberAsync(user, user.PhoneNumber, token);

                if (result.Succeeded)
                {
                    TempData["SweetAlert"] = JsonConvert.SerializeObject(new SweetAlert()
                    {
                        Title = "تایید شماره موبایل",
                        Text = "شماره تلفن شما با موفقیت تایید شد",
                        Icon = SweetAlertIcon.success,
                        ShowCloseButton = false,
                        CancelButtonText = "",
                        ComfirmButtonText = "حله",
                        ShowCancelButton = false
                    });


                    return RedirectToAction("Index", "Profile");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        errors += error.Description + "\n";
                    }
                }
            }
            else
            {
                errors += "لطفا کد امنیتی را صحیح وارد کنید";
            }
            ViewData["Errors"] = errors;
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> VerifyEmail()
        {
            var user = await _userManager.GetUserAsync(User);
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                var emailComfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var emailMessage = Url.Action("ComfirmEmail", "Account", new { userName = user.UserName, token = emailComfirmationToken }, Request.Scheme);
                var body = await this.RenderViewAsync("~/Views/EmailTemplates/EmailComfirmationTemplate.cshtml", emailMessage);

                var emailResult = await _emailSender.SendEmailAsync(EmailSetting.TestEmail, "Email Comfirmation", body, user.Email, true);

                if (emailResult.IsSuccess)
                {
                    TempData["SweetAlert"] = JsonConvert.SerializeObject(new SweetAlert()
                    {
                        Title = "تایید ایمیل",
                        Text = "ایمیل تایید برای شما ارسال شد",
                        Icon = SweetAlertIcon.success,
                        ShowCloseButton = false,
                        CancelButtonText = "",
                        ComfirmButtonText = "حله",
                        ShowCancelButton = false
                    });
                }
                else
                {
                    string errorMsg = "خطایی در ارسال ایمیل تایید بوجود آمد.";

                    foreach (var error in emailResult.Errors)
                    {
                        errorMsg += "\n" + error + ". ";
                    }
                    TempData["SweetAlert"] = JsonConvert.SerializeObject(new SweetAlert()
                    {
                        Title = "بروز خطا",
                        Text = errorMsg,
                        Icon = SweetAlertIcon.error,
                        ShowCloseButton = false,
                        CancelButtonText = "",
                        ComfirmButtonText = "حله",
                        ShowCancelButton = false
                    });
                }
            }


            return RedirectToAction("Index", "Profile");
        }



    }
}
