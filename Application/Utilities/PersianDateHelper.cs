using Application.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Application.Utilities
{
  public static  class PersianDateHelper
    {
        public static DateTime ConvertToDate(int persianYear,int persianMonth,int persianDay,int persianHour=0,int persianMinute=0,int persianSecound=0,int persianMiliSecound=0)
        {
            PersianCalendar pc = new PersianCalendar();
            
            return pc.ToDateTime(persianYear, persianMonth, persianDay, persianHour, persianMinute, persianSecound, persianMiliSecound);
        }

        public static int GetPersianDay(DateTime dt)
        {
            PersianCalendar pc = new PersianCalendar();

            return pc.GetDayOfMonth(dt);
        }

        public static int GetPersianMonth(DateTime dt)
        {
            PersianCalendar pc = new PersianCalendar();

            return pc.GetMonth(dt);
        }

        public static string GetPersianMonthName(DateTime dt)
        {
            PersianCalendar pc = new PersianCalendar();

            return ((PersianMonthOfYear)pc.GetMonth(dt)).ToString();
        }

        public static int GetPersianYear(DateTime dt)
        {
            PersianCalendar pc = new PersianCalendar();

            return pc.GetYear(dt);
        }
    }
}
