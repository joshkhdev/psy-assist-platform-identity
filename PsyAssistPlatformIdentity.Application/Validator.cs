using System.Text.RegularExpressions;

namespace PsyAssistPlatformIdentity.Application;

public static class Validator
{
    public static bool EmailValidator(string email)
    {
        const string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(email, pattern);
    }

    public static bool PhoneNumberValidator(string phone)
    {
        const string pattern = @"^\+\d{11,18}$";
        return Regex.IsMatch(phone, pattern);
    }
}