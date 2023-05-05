using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryDatabase.Utils
{
    public class DateUtils
    {
        private const string ES_DATE = "dd/MM/yyyy";

        public static DateTime DateFromStringES(String date)
        {
            return DateTime.ParseExact(date, ES_DATE, CultureInfo.InvariantCulture);
        }
    }
}
