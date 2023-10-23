using Pepegov.MicroserviceFramework.Data;

namespace Pepegov.MicroserviceFramework.Infrastructure.Extensions;

public static class StringExtensions
{
    public static Guid ToGuid(this string str)
    {
        Guid.TryParse(str, out var result);
        return result;
    }
    
    public static bool IsWebUrl(this string target)
        => !string.IsNullOrEmpty(target) && Expressions.Web.Url.IsMatch(target);

    public static bool IsEmail(this string target)
        => !string.IsNullOrEmpty(target) && Expressions.Web.Email.IsMatch(target);
    
    public static bool IsIPv4(this string target)
        => !string.IsNullOrEmpty(target) && Expressions.Web.IPv4.IsMatch(target);
    
    public static bool IsIPv6(this string target)
        => !string.IsNullOrEmpty(target) && Expressions.Web.IPv6.IsMatch(target);
    
    public static bool IsCreditCardNumber(this string target)
        => !string.IsNullOrEmpty(target) && Expressions.PersonalData.CreditCardNumberExpression.IsMatch(target);

    public static bool IsPhoneNumberWithoutMask(this string target)
        => !string.IsNullOrEmpty(target) && Expressions.PersonalData.PhoneNumberWithoutMaskExpression.IsMatch(target);
}