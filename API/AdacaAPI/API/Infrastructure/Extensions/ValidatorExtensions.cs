using System.Text.RegularExpressions;

namespace Adaca.Api.Infrastructure.Extensions
{
    public static class ValidatorExtensions
    {
        public static bool IsValidEmailAddress(this string s)
        {
            var regex = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
            return regex.IsMatch(s);
        }
        public static bool IsValidPhoneNumber(this string s)
        {
            var regex = new  Regex(@"^(\+?\(61\)|\(\+?61\)|\+?61|\(0[1-9]\)|0[1-9])?( ?-?[0-9]){7,9}$");
            return regex.IsMatch(s);
        }
        public static bool IsValidBusinessNumberFormat(this string s)
        {
            var regex = new Regex(@"^(\d *?){11}$");
            return regex.IsMatch(s);
        }

    }
}
