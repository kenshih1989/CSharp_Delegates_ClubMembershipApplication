using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FieldValidatorAPI
{
    #region Delegate definitions
    public delegate bool RequiredFieldValidatorDelegate(string fieldValue);
    public delegate bool StringLengthValidatorDelegate(string fieldValue, int minLength, int maxLength);
    public delegate bool DateOfBirthValidatorDelegate(string fieldValue, out DateTime validDateTime);
    public delegate bool PaternMatcherValidatorDelegate(string fieldValue, string pattern);
    public delegate bool CompareFieldsValidatorDelegate(string fieldValue, string fieldValueToCompare);
    #endregion
    internal class CommonFiledValidatorFunctions
    {

        #region The lazy initialization of the delegate instances for each type of validation

        // The private static fields that hold the delegate instances for each type of validation
        private static RequiredFieldValidatorDelegate _requiredFieldValidator = null;
        private static StringLengthValidatorDelegate _stringLengthValidator = null;
        private static DateOfBirthValidatorDelegate _dateOfBirthValidator = null;
        private static PaternMatcherValidatorDelegate _paternMatcherValidator = null;
        private static CompareFieldsValidatorDelegate _compareFieldsValidator = null;

        // The public static properties that provide access to the delegate instances for each type of validation, using lazy initialization
        public static RequiredFieldValidatorDelegate RequiredFieldValidatorDelegate
        {
            get
            {
                if (_requiredFieldValidator == null)
                    _requiredFieldValidator = new RequiredFieldValidatorDelegate(IsRequiredFieldValid);
                return _requiredFieldValidator;
            }
        }

        public static StringLengthValidatorDelegate StringLengthValidatorDelegate
        {
            get
            {
                if (_stringLengthValidator == null)
                    _stringLengthValidator = new StringLengthValidatorDelegate(IsStringLengthValid);
                return _stringLengthValidator;
            }
        }

        public static DateOfBirthValidatorDelegate DateOfBirthValidatorDelegate
        {
            get
            {
                if (_dateOfBirthValidator == null)
                    _dateOfBirthValidator = new DateOfBirthValidatorDelegate(IsDateOfBirthValid);
                return _dateOfBirthValidator;
            }
        }

        public static PaternMatcherValidatorDelegate PaternMatcherValidatorDelegate
        {
            get
            {
                if (_paternMatcherValidator == null)
                    _paternMatcherValidator = new PaternMatcherValidatorDelegate(IsPaternMatchValid);
                return _paternMatcherValidator;
            }
        }

        public static CompareFieldsValidatorDelegate CompareFieldsValidatorDelegate
        {
            get
            {
                if (_compareFieldsValidator == null)
                    _compareFieldsValidator = new CompareFieldsValidatorDelegate(IsCompareFieldsValid);
                return _compareFieldsValidator;
            }
        }
        #endregion


        #region The private static methods that implement the actual validation logic for each type of validation
        private static bool IsRequiredFieldValid(string fieldValue)
        {
            if (!String.IsNullOrEmpty(fieldValue))
                return true;
            return false;
        }

        private static bool IsStringLengthValid(string fieldValue, int minLength, int maxLength)
        {
            if (fieldValue.Length >= minLength && fieldValue.Length <= maxLength)
                return true;
            return false;
        }

        private static bool IsDateOfBirthValid(string dateTime, out DateTime validDateTime)
        {
            if (DateTime.TryParse(dateTime, out validDateTime))
                return true;
            return false;
        }

        private static bool IsPaternMatchValid(string fieldValue, string regularExpressionPattern)
        {
            Regex regex = new Regex(regularExpressionPattern);

            if (regex.IsMatch(fieldValue))
                return true;
            return false;
        }

        private static bool IsCompareFieldsValid(string fieldValue, string fieldValueToCompare)
        {
            if (fieldValue.Equals(fieldValueToCompare))
                return true;
            return false;
        }
        #endregion

    }
}
