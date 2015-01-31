using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Model
{
    class SecondaryDateTime
    {
        public static DateTime GetMonday(DateTime dtNow)
        {
            if (dtNow.DayOfWeek == DayOfWeek.Saturday || dtNow.DayOfWeek == DayOfWeek.Sunday)
            {
                do
                {
                    dtNow = dtNow.AddDays(-1);
                }
                while (dtNow.DayOfWeek != DayOfWeek.Monday);
            }
            else
            {
                if (dtNow.DayOfWeek != DayOfWeek.Monday)
                {
                    do
                    {
                        dtNow = dtNow.AddDays(-1);
                    }
                    while (dtNow.DayOfWeek != DayOfWeek.Monday);
                }
            }
            return dtNow;
        }
    }
}
