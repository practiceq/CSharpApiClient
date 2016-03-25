using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntakeQ.ApiClient.Helpers
{
    public static class ExtendionMethods
    {
        public static string ToQueryString(this NameValueCollection nvc)
        {
            if (nvc.Count == 0) return String.Empty;

            var array = (from key in nvc.AllKeys
                         from value in nvc.GetValues(key)
                         select $"{Uri.EscapeDataString(key)}={Uri.EscapeDataString(value)}")
                .ToArray();
            return "?" + string.Join("&", array);
        }
    }
}
