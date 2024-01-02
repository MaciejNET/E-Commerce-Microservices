namespace ECommerce.Services.Users.Core.Validators;

internal class PasswordValidator
{
    public bool Validate(string password)
    {
        var hasUpperCase = false;
        var hasLowerCase = false;
        var hasDigit = false;
        var hasSpecialChar = false;

        if (password.Length < 8) return false;

        foreach (var c in password)
            if (char.IsUpper(c))
                hasUpperCase = true;
            else if (char.IsLower(c))
                hasLowerCase = true;
            else if (char.IsDigit(c))
                hasDigit = true;
            else if (char.IsPunctuation(c) || char.IsSymbol(c))
                hasSpecialChar = true;

        return hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar;
    }
}