using System.Text.RegularExpressions;
using WalksInfoViberBot.Exceptions;

namespace WalksInfoViberBot.Services
{
    public static class ValidationService
    {
        public static void ValidateImei(string imei)
        {
            if (!Regex.IsMatch(imei, @"^\d{15}$"))
            {
                throw new BusinessException($"Invalid IMEI {imei}.");
            }
        }
    }
}
