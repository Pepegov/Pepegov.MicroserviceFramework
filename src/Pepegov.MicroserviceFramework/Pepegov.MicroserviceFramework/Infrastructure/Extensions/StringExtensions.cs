using System.Text.RegularExpressions;

namespace Pepegov.MicroserviceFramework.Infrastructure.Extensions;

public static class StringExtensions
{
    private static readonly Regex WebUrlExpression = new Regex(@"((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+(:[0-9]+)?|(?:www.|[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)", RegexOptions.Singleline | RegexOptions.Compiled);
    private static readonly Regex EmailExpression = new Regex(@"^([0-9a-zA-Z]+[-._+&])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$", RegexOptions.Singleline | RegexOptions.Compiled);
    private static readonly Regex IPv4Expression = new Regex(@"^\b(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b$", RegexOptions.Singleline | RegexOptions.Compiled);
    private static readonly Regex IPv6Expression = new Regex(@"^(([0-9a-fA-F]{1,4}:){7,7}[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,7}:|([0-9a-fA-F]{1,4}:){1,6}:[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,5}(:[0-9a-fA-F]{1,4}){1,2}|([0-9a-fA-F]{1,4}:){1,4}(:[0-9a-fA-F]{1,4}){1,3}|([0-9a-fA-F]{1,4}:){1,3}(:[0-9a-fA-F]{1,4}){1,4}|([0-9a-fA-F]{1,4}:){1,2}(:[0-9a-fA-F]{1,4}){1,5}|[0-9a-fA-F]{1,4}:((:[0-9a-fA-F]{1,4}){1,6})|:((:[0-9a-fA-F]{1,4}){1,7}|:)|fe80:(:[0-9a-fA-F]{0,4}){0,4}%[0-9a-zA-Z]{1,}|::(ffff(:0{1,4}){0,1}:){0,1}((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])|([0-9a-fA-F]{1,4}:){1,4}:((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9]))$", RegexOptions.Singleline | RegexOptions.Compiled);
    private static readonly Regex CreditCardNumberExpression = new Regex(@"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$", RegexOptions.Singleline | RegexOptions.Compiled);
    private static readonly Regex PhoneNumberWithoutMaskExpression = new Regex(@"^(\+)(\d){10,14}$", RegexOptions.Singleline | RegexOptions.Compiled);
    
    public static Guid ToGuid(this string str)
    {
        Guid.TryParse(str, out var result);
        return result;
    }
    
    public static bool IsWebUrl(this string target)
        => !string.IsNullOrEmpty(target) && WebUrlExpression.IsMatch(target);

    public static bool IsEmail(this string target)
        => !string.IsNullOrEmpty(target) && EmailExpression.IsMatch(target);
    
    public static bool IsIPv4(this string target)
        => !string.IsNullOrEmpty(target) && IPv4Expression.IsMatch(target);
    
    public static bool IsIPv6(this string target)
        => !string.IsNullOrEmpty(target) && IPv6Expression.IsMatch(target);
    
    public static bool IsCreditCardNumber(this string target)
        => !string.IsNullOrEmpty(target) && CreditCardNumberExpression.IsMatch(target);

    public static bool IsPhoneNumberWithoutMask(this string target)
        => !string.IsNullOrEmpty(target) && PhoneNumberWithoutMaskExpression.IsMatch(target);
}