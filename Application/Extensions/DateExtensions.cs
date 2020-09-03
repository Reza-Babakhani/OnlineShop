using Application.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Application.Extensions
{
    public static class DateExtensions
    {

        public static string ToPersianDate(this DateTime dt)
        {
            string result = "";
            PersianCalendar pc = new PersianCalendar();

            result += pc.GetYear(dt) + "/";
            result += pc.GetMonth(dt) + "/";
            result += pc.GetDayOfMonth(dt);

            return result;
        }

        public static string ToFarsiPersianDate(this DateTime dt, bool showDayOfWeek)
        {
            string result = "";
            PersianCalendar pc = new PersianCalendar();

            if (showDayOfWeek)
            {
                PersianDayOfWeek pdow = (PersianDayOfWeek)((int)pc.GetDayOfWeek(dt));
                result += pdow.ToString() + " ";
            }
            result += pc.GetDayOfMonth(dt) + " ";
            result += ((PersianMonthOfYear)pc.GetMonth(dt)).ToString() + " ";
            result += pc.GetYear(dt);

            return result;
        }

    }
}
