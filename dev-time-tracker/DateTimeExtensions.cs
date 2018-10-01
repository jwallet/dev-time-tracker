using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevTimeTracker
{
    internal static class DateTimeExtensions
    {
        internal static string ToTimeFormat(this DateTime dt)
        {
            return dt.ToString(Properties.Settings.Default.TimeFormat);
        }
    }
}
