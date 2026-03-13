using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldValidatorAPI
{
    static class CommonRegularExpressionValidationPatterns
    {
        // The regular expression pattern for validating an email address,
        // which consists of a local part (before the @ symbol)
        // and a domain part (after the @ symbol).
        public const string Email_Address_RegEx_Pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

        // The regular expression pattern for validating a Malaysian mobile phone number,
        // which starts with +60 or 01, followed by a digit from 0 to 9 (except 5),
        // there is an optional hyphen after the first 3 or 4 digits (depending on whether the country code is included or not),
        // and then followed by 7 or 8 digits.
        // The pattern also allows for optional hyphens between the digits.
        public const string My_Mobile_PhoneNumber_RegEx_Pattern = @"^(\+?6?01)[0-46-9]-?[0-9]{7,8}$";

        // The regular expression pattern for validating a Malaysian postal code, which consists of exactly 5 digits.
        public const string My_Post_Code_RegEx_Pattern = @"^\d{5}$";

        // The regular expression pattern for validating a strong password,
        // which must contain at least one lowercase letter, one uppercase letter, one digit, and one special character,
        // and must be between 6 and 10 characters long, and must not contain any whitespace characters.
        public const string Strong_Password_RegEx_Pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+}{"":;'?/>.<,])(?!.*\s).{6,10}$";
    }
}
