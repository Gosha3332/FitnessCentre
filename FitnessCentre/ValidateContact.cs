using System.Text.RegularExpressions;

public static class ValidateContact
{
    public static bool Email(string email)
    {
        string regex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.(ru|com|org|net)$";
        return Regex.IsMatch(email, regex);
    }

    public static bool Phone(string phone)
    {
        string regex = @"^([+]7|8)([\(]\d{3}[\)]|\d{3})(\d{3}[-]\d{2}[-]\d{2}|\d{7})$";
        return Regex.IsMatch(phone, regex);
    }

    public static bool DateBirthday(DateTime date)
    {
        return date < DateTime.Now;
    }
}
