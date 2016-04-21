using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Infrastructure.Internationalization
{
    public static class TranslatableListExtension
    {
        public static T GetCurrentLocaleContent<T>(this IEnumerable<T> list)
            where T : class, ITranslatable
        {
            var currentLocale = Thread.CurrentThread.CurrentUICulture.Name;
            var content = list.FirstOrDefault(p => p.Locale == currentLocale);
            if (content != null)
                return content;

            content = list.FirstOrDefault(p => p.Locale == "en-US");
            if (content != null)
                return content;

            return list.FirstOrDefault();
        }
    }
}
