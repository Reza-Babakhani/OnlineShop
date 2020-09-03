using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Utilities;
using Application.ViewModels.Profile;
using Domain.Models;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Presentation.Mvc.Extensions;
using Presentation.Mvc.Models;

namespace Presentation.Mvc.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public ProfileController(UserManager<ApplicationUser> userManager,IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        private async Task<ProfileSideBarViewModel> GetSideBarViewModel()
        {
            var user = await _userManager.GetUserAsync(User);

            ProfileSideBarViewModel model = new ProfileSideBarViewModel();

            model.Email = user.Email;
            model.FullName = user.FirstName + " " + user.LastName;
            model.ImagePath = user.ImagePath;
            model.MobileNumber = user.PhoneNumber;

            return model;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            ProfileIndexViewModel model = new ProfileIndexViewModel();
            //
            model.ProfileInformations.CreditCardNumber = user.CreditCardNumber;
            model.ProfileInformations.Email = user.Email;
            model.ProfileInformations.FullName = user.FirstName + " " + user.LastName;
            model.ProfileInformations.MobileNumber = user.PhoneNumber;
            model.ProfileInformations.NewsSubscribe = user.NewsSubscribe;
            model.ProfileInformations.PersonalCode = user.PersonalCode;
            //
            model.SideBarViewModel = await GetSideBarViewModel();
            //

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditPersonalInfo()
        {
            var user = await _userManager.GetUserAsync(User);

            ProfileEditDetails model = new ProfileEditDetails();
            //
            model.CreditCardNumber = user.CreditCardNumber;
            model.Email = user.Email;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.MobileNumber = user.PhoneNumber;
            model.NewsSubscribe = user.NewsSubscribe;
            model.PersonalCode = user.PersonalCode;
            if(user.BirthDay.Year>=650 && user.BirthDay.Month!=0 && user.BirthDay.Day != 0)
            {
                model.BirthDayDay = PersianDateHelper.GetPersianDay(user.BirthDay);
                model.BirthDayMonth = PersianDateHelper.GetPersianMonth(user.BirthDay);
                model.BirthDayYear = PersianDateHelper.GetPersianYear(user.BirthDay);
            }

            //

            ViewBag.SideBarModel = await GetSideBarViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPersonalInfo(ProfileEditDetails model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                user.BirthDay = PersianDateHelper.ConvertToDate(model.BirthDayYear, model.BirthDayMonth, model.BirthDayDay);
                user.CreditCardNumber = model.CreditCardNumber;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.NewsSubscribe = model.NewsSubscribe;
                user.PersonalCode = model.PersonalCode;

                if (user.Email != model.Email)
                {
                    user.Email = model.Email;
                    user.EmailConfirmed = false;
                    user.NormalizedEmail = _userManager.NormalizeEmail(model.Email);

                }

                if (user.PhoneNumber != model.MobileNumber)
                {
                    user.PhoneNumber = model.MobileNumber;
                    user.PhoneNumberConfirmed = false;            
                }


                var result=  await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    ///Send comfirmation email
                    var emailComfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var emailMessage = Url.Action("ComfirmEmail", "Account", new { userName = user.UserName, token = emailComfirmationToken }, Request.Scheme);
                    var body = await this.RenderViewAsync("~/Views/EmailTemplates/EmailComfirmationTemplate.cshtml", emailMessage);

                    var emailResult = await _emailSender.SendEmailAsync(EmailSetting.TestEmail, "Email Comfirmation", body, user.Email, true);
                    ///


                    TempData["SweetAlert"] = JsonConvert.SerializeObject(new SweetAlert()
                    {
                        Title = "ثبت تغییرات",
                        Text = "اطلاعات حساب کاربری با موفقیت تغییر کرد",
                        Icon = SweetAlertIcon.success,
                        ShowCloseButton = false,
                        CancelButtonText = "",
                        ComfirmButtonText = "حله",
                        ShowCancelButton = false
                    });
                  return  RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);

                    }
                }
                
            }
            ViewBag.SideBarModel = await GetSideBarViewModel();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }
    }
}
