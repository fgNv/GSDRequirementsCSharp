using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSDRequirementsCSharp.Domain.Globals
{
    public static class DateTimeExtensions
    {
        public static string RelativeTime(this DateTime date)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = new TimeSpan(DateTime.Now.Ticks - date.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
                return ts.Seconds == 1 ? Sentences.oneSecondAgo : $"{ts.Seconds} {Sentences.secondsAgo}";

            if (delta < 2 * MINUTE)
                return Sentences.aMinuteAgo;

            if (delta < 45 * MINUTE)
                return $"{ts.Minutes} {Sentences.minutesAgo}";

            if (delta < 90 * MINUTE)
                return Sentences.anHourAgo;

            if (delta < 24 * HOUR)
                return $"{ts.Hours} {Sentences.hoursAgo}";

            if (delta < 48 * HOUR)
                return Sentences.yesterday;

            if (delta < 30 * DAY)
                return $"{ts.Days} {Sentences.daysAgo}";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? Sentences.oneMonthAgo : $"{months} {Sentences.monthsAgo}";
            }

            int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
            return years <= 1 ? Sentences.oneYearAgo : $"{years} {Sentences.yearsAgo}";
        }
    }
}