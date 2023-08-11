using System.Text.RegularExpressions;

namespace Integracao.CPTEC.Domain.Utils
{
    public static class Helper
    {
        public static bool IsNotValidIcao(string icaoCode)
        {
            if (string.IsNullOrEmpty(icaoCode)) return true;

            return !Regex.IsMatch(icaoCode.Trim().ToUpper(), @"^SB[A-Za-z]{2}$");
        }
        public static bool IsNotValidString(string value)
        {
            if (string.IsNullOrEmpty(value)) return true;

            return Regex.IsMatch(value, @"\d");
        }
    }
}
