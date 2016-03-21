using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeViewMobile.Extensions
{
    public static class Utilities
    {
        public static bool IsValidEmail(string email)
        {
            try
            {
                if (email.Contains("@") && email.Contains(".") && email.Length > 5) {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
