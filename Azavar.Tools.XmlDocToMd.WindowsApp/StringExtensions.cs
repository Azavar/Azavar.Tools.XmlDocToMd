using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azavar.Tools.XmlDocToMd.WindowsApp
{
    public static class StringExtensions
    {
        public static string ShortFileName(this string s)
        {
            if (s.Length < 50)
                return s;
            return s.Substring(0, 23) + "..." + s.Substring((s.Length - 24), 24);
        }
    }
}
