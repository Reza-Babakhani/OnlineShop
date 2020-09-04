using Application.Interfaces;
using Domain.Models;
using SmsIrRestfulNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Services
{
    public class SmsSender : ISmsSender
    {
        public  string GetToken(SmsSetting ss)
        {
            SmsIrRestfulNetCore.Token tk = new SmsIrRestfulNetCore.Token();
            return tk.GetToken(ss.ApiKey, ss.SecurityCode);
        }

        public  bool IsCreditSuccessful(SmsSetting ss)
        {
            CreditResponse credit = new Credit().GetCredit(GetToken(ss));
            if (credit != null)
            {
                return credit.IsSuccessful;

            }
            return false;
        }

        public  double GetCredit(SmsSetting ss)
        {

            CreditResponse credit = new Credit().GetCredit(GetToken(ss));
            if (IsCreditSuccessful(ss))
                if (credit != null)
                    return credit.Credit;
            return -1;


        }

        public  IList<string> GetLineNumbers(SmsSetting ss)
        {

            var numbers = new SmsLine().GetSmsLines(GetToken(ss));

            return numbers.SMSLines.Select(n => n.LineNumber.ToString()).ToList();

        }


        public  bool SendMessage(string message, string[] mobiles, string lineNumber, SmsSetting ss)
        {


            var messageSendObject = new MessageSendObject()
            {
                Messages = new List<string> { message }.ToArray(),
                MobileNumbers = mobiles,
                LineNumber = lineNumber,
                SendDateTime = null,
                CanContinueInCaseOfError = true
            };

            MessageSendResponseObject messageSendResponseObject = new MessageSend().Send(GetToken(ss), messageSendObject);

            return messageSendResponseObject.IsSuccessful;


        }


        public  SentMessage[] GetSentMessage(SmsSetting ss, int pageNumber, string fromDate, string toDate)
        {

            var sentMessageResponseByDate = new MessageSend().GetByDate(GetToken(ss), fromDate, toDate, 20, pageNumber);

            return sentMessageResponseByDate.Messages;

        }

        public  ReceivedMessages[] GetReceivedMessage(SmsSetting ss, int pageNumber, string fromDate, string toDate)
        {

            var receivedMessageResponseByDate = new ReceiveMessage().GetByDate(GetToken(ss), fromDate, toDate, 10, pageNumber);

            return receivedMessageResponseByDate.Messages;

        }
    }
}
