using Domain.Models;
using SmsIrRestfulNetCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
  public  interface ISmsSender
    {
        string GetToken(SmsSetting ss);

        bool IsCreditSuccessful(SmsSetting ss);

        double GetCredit(SmsSetting ss);

        IList<string> GetLineNumbers(SmsSetting ss);

        OperationResult SendMessage(string message, string[] mobiles, string lineNumber, SmsSetting ss);

        SentMessage[] GetSentMessage(SmsSetting ss, int pageNumber, string fromDate, string toDate);

        ReceivedMessages[] GetReceivedMessage(SmsSetting ss, int pageNumber, string fromDate, string toDate);
    }
}
