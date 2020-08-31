using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Models;

namespace Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task<OperationResult> SendEmailAsync(EmailSetting email, string subject, string body, string[] toEmails, bool IsHtmlBody = false, bool EnableSsl = true)
        {
            OperationResult result = new OperationResult();

            using (MailMessage mail = new MailMessage())
            {
                using (SmtpClient SmtpServer = new SmtpClient(email.Host))
                {
                    mail.From = new MailAddress(email.Address, email.DisplayName);
                    foreach (var toEmail in toEmails)
                    {
                        mail.To.Add(toEmail);
                    }

                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = IsHtmlBody;
                    SmtpServer.Port = email.Port;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(email.UserName, email.Password);
                    SmtpServer.EnableSsl = EnableSsl;

                    SmtpServer.SendCompleted += (s, e) =>
                    {
                        // Get the unique identifier for this asynchronous operation.
                       // string token = (string)e.UserState;

                        if (e.Cancelled)
                        {
                            result.Errors.Add(new OperationError { Code = "Email Send Canceled", Description = $"Send canceled." });
                        }
                        if (e.Error != null)
                        {
                            result.Errors.Add(new OperationError { Code = "Email Send Error", Description = $"Message:{e.Error.Message}" });
                        }
                        else
                        {
                            result.IsSuccess = true;
                        }

                    };


                    await SmtpServer.SendMailAsync(mail);

                    return result;
                }
            }

        }

        public async Task<OperationResult> SendEmailAsync(EmailSetting email, string subject, string body, string toEmail, bool IsHtmlBody = false, bool EnableSsl = true)
        {
            OperationResult result = new OperationResult();

            using (MailMessage mail = new MailMessage())
            {
                using (SmtpClient SmtpServer = new SmtpClient(email.Host))
                {
                    mail.From = new MailAddress(email.Address, email.DisplayName);

                    mail.To.Add(toEmail);

                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = IsHtmlBody;
                    SmtpServer.Port = email.Port;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(email.UserName, email.Password);
                    SmtpServer.EnableSsl = EnableSsl;

                    SmtpServer.SendCompleted += (s, e) =>
                    {
                        // Get the unique identifier for this asynchronous operation.
                       // String token = (string)e.UserState;

                        if (e.Cancelled)
                        {
                            result.Errors.Add(new OperationError { Code = "Email Send Canceled", Description = $"{toEmail} Send canceled." });
                        }
                        if (e.Error != null)
                        {
                            result.Errors.Add(new OperationError { Code = "Email Send Error", Description = $"Email:{toEmail} Message:{e.Error.Message}" });
                        }
                        else
                        {
                            result.IsSuccess = true;
                        }

                    };


                    await SmtpServer.SendMailAsync(mail);

                    return result;
                }
            }

        }
    }
}
