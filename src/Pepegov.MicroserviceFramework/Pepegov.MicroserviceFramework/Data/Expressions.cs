using System.Text.RegularExpressions;

namespace Pepegov.MicroserviceFramework.Data;

public static class Expressions
{
    public static class Web
    {
        public static readonly Regex Url = new Regex(@"((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+(:[0-9]+)?|(?:www.|[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)", RegexOptions.Singleline | RegexOptions.Compiled);
        public static readonly Regex Email = new Regex(@"^([0-9a-zA-Z]+[-._+&])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$", RegexOptions.Singleline | RegexOptions.Compiled);
        public static readonly Regex IPv4 = new Regex(@"^\b(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b$", RegexOptions.Singleline | RegexOptions.Compiled);
        public static readonly Regex IPv6 = new Regex(@"^(([0-9a-fA-F]{1,4}:){7,7}[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,7}:|([0-9a-fA-F]{1,4}:){1,6}:[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,5}(:[0-9a-fA-F]{1,4}){1,2}|([0-9a-fA-F]{1,4}:){1,4}(:[0-9a-fA-F]{1,4}){1,3}|([0-9a-fA-F]{1,4}:){1,3}(:[0-9a-fA-F]{1,4}){1,4}|([0-9a-fA-F]{1,4}:){1,2}(:[0-9a-fA-F]{1,4}){1,5}|[0-9a-fA-F]{1,4}:((:[0-9a-fA-F]{1,4}){1,6})|:((:[0-9a-fA-F]{1,4}){1,7}|:)|fe80:(:[0-9a-fA-F]{0,4}){0,4}%[0-9a-zA-Z]{1,}|::(ffff(:0{1,4}){0,1}:){0,1}((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])|([0-9a-fA-F]{1,4}:){1,4}:((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9]))$", RegexOptions.Singleline | RegexOptions.Compiled);
    }

    public static class Paths
    {
        /// <summary>
        /// Appropriate values:
        /// .\ ..\ \ ..\a .\a \a \a\a \a\a.a U: U:\ U:\\ U:\\\ U:\a U:\\a U:\a.a U:\\a.a U:\a\a U:\\a\a 
        /// </summary>
        public static readonly Regex Windows = new Regex(
            @"(^([a-z]|[A-Z]):(?=\\(?![\0-\37<>:"+"\""
                 +@"/\\|?*])|\/(?![\0-\37<>:"+"\""+@"/\\|?*])|$)|^\\(?=[\\\/][^\0-\37<>:"+"\""
                 +@"/\\|?*]+)|^(?=(\\|\/)$)|^\.(?=(\\|\/)$)|^\.\.(?=(\\|\/)$)|^(?=(\\|\/)[^\0-\37<>:"+"\""
                 +@"/\\|?*]+)|^\.(?=(\\|\/)[^\0-\37<>:"+"\""
                 +@"/\\|?*]+)|^\.\.(?=(\\|\/)[^\0-\37<>:"+"\""
                 +@"/\\|?*]+))((\\|\/)[^\0-\37<>:"+"\""+
                 @"/\\|?*]+|(\\|\/)$)*()$", RegexOptions.Singleline | RegexOptions.Compiled);
        public static readonly Regex Linux = new Regex(@"^\/$|(^(?=\/)|^\.|^\.\.)(\/(?=[^/\0])[^/\0]+)*\/?$", RegexOptions.Singleline | RegexOptions.Compiled);
    }

    public static class PersonalData
    {
        public static readonly Regex CreditCardNumberExpression = new Regex(@"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$", RegexOptions.Singleline | RegexOptions.Compiled);
        public static readonly Regex PhoneNumberWithoutMaskExpression = new Regex(@"^(\+)(\d){10,14}$", RegexOptions.Singleline | RegexOptions.Compiled);   
    }
}