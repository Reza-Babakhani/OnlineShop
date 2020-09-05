using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Models;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentation.Mvc.Extensions;
using Presentation.Mvc.Models;

namespace Presentation.Mvc.Controllers
{

    public class MessageSenderController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IAntiforgery _antiforgery;

        public MessageSenderController( UserManager<ApplicationUser> userManager, IEmailSender emailSender,IAntiforgery antiforgery)
        {
           
            _userManager = userManager;
            _antiforgery = antiforgery;
            _emailSender = emailSender;

        }

       [HttpPost]
        public async Task<IActionResult> SendEmailComfirmationMessage(string userId, string __RequestVerificationToken,string returnUrl = "")
        {
            //string originalToken = _antiforgery.GetTokens(HttpContext).RequestToken;

            //if (originalToken != __RequestVerificationToken)
            //{
            //    return BadRequest();
            //}

            if (string.IsNullOrEmpty(userId))
                return NotFound();

            var user =await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            var emailComfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var emailMessage = Url.Action("ConfirmEmail", "Account", new { userName = user.UserName, token = emailComfirmationToken }, Request.Scheme);
            var body = await this.RenderViewAsync("~/Views/EmailTemplates/EmailConfirmationTemplate.cshtml", emailMessage);

            var sendResult = await _emailSender.SendEmailAsync(EmailSetting.TestEmail, "Email Confirmation", body, user.Email, true);

            if (sendResult.IsSuccess)
            {
                TempData["SweetAlert"] = JsonConvert.SerializeObject(new SweetAlert()
                {
                    Title = "ارسال تاییدیه",
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

                foreach (var error in sendResult.Errors)
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


            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }





    }
}
