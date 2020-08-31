using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IEmailSender
    {
        Task<OperationResult> SendEmailAsync(EmailSetting email, string subject, string body, string[] toEmails, bool IsHtmlBody = false, bool EnableSsl = true);

        Task<OperationResult> SendEmailAsync(EmailSetting email, string subject, string body, string toEmail, bool IsHtmlBody = false, bool EnableSsl = true);

    }
}
