using ClubMembershipApplication.Data;
using FieldValidatorAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubMembershipApplication.FieldValidators
{
    internal class UserRegistrationValidator : IFieldValidator
    {
        /// The constants for the minimum and maximum length of the first name and last name fields
        const int FirstNameMinLength = 2;
        const int FirstNameMaxLength = 100;
        const int LastNameMinLength = 2;
        const int LastNameMaxLength = 100;

        // The delegate instance for validating the email address field, which will be assigned to a method that checks if the email address already exists in the database
        delegate bool EmailExistDelegate(string emailAddress);
        private EmailExistDelegate _emailExistDelegate = null;

        private RequiredFieldValidatorDelegate _requiredFieldValidator = null;
        private StringLengthValidatorDelegate _stringLengthValidator = null;
        private DateOfBirthValidatorDelegate _dateOfBirthValidator = null;
        private PatternMatcherValidatorDelegate _paternMatcherValidator = null;
        private CompareFieldsValidatorDelegate _compareFieldsValidator = null;

        private FieldValidatorDelegate _fieldValidatorDelegate = null;
        private IRegister _register = null;

        string[] _fieldArray = null;
        public string[] FieldArray
        {
            get
            {
                if (_fieldArray == null)
                    _fieldArray = new string[Enum.GetValues(typeof(FieldConstants.UserRegistrationField)).Length];
                return _fieldArray;
            }
        }

        public FieldValidatorDelegate GetFieldValidator => _fieldValidatorDelegate;

        public UserRegistrationValidator(IRegister register)
        {
            _register = register;
        }

        public void InitializeFieldValidatorsDelegates()
        {
            _requiredFieldValidator = CommonFieldValidatorFunctions.RequiredFieldValidatorDelegate;
            _stringLengthValidator = CommonFieldValidatorFunctions.StringLengthValidatorDelegate;
            _dateOfBirthValidator = CommonFieldValidatorFunctions.DateOfBirthValidatorDelegate;
            _paternMatcherValidator = CommonFieldValidatorFunctions.PaternMatcherValidatorDelegate;
            _compareFieldsValidator = CommonFieldValidatorFunctions.CompareFieldsValidatorDelegate;

            _fieldValidatorDelegate = new FieldValidatorDelegate(IsFieldValid);
            _emailExistDelegate = new EmailExistDelegate(_register.EmailAddressExists);
        }

        private bool IsFieldValid(int fieldIndex, string fieldValue, string[] fieldArray, out string fieldInvalidMessage)
        {
            fieldInvalidMessage = string.Empty;

            FieldConstants.UserRegistrationField userRegistrationField = (FieldConstants.UserRegistrationField)fieldIndex;

            switch (userRegistrationField)
            {
                case FieldConstants.UserRegistrationField.EmailAddress:
                    fieldInvalidMessage =
                        (!_requiredFieldValidator(fieldValue)) ?
                        $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" :
                        "";
                    fieldInvalidMessage =
                        (fieldInvalidMessage == "" && !_paternMatcherValidator(fieldValue, CommonRegularExpressionValidationPatterns.Email_Address_RegEx_Pattern))
                        ? $"You must enter a valid email address{Environment.NewLine}"
                        : fieldInvalidMessage;
                    fieldInvalidMessage =
                        (fieldInvalidMessage == "" && _emailExistDelegate(fieldValue))
                        ? $"This email address already exists. Please try again{Environment.NewLine}"
                        : fieldInvalidMessage;
                    break;

                case FieldConstants.UserRegistrationField.FirstName:
                    fieldInvalidMessage =
                        (!_requiredFieldValidator(fieldValue)) ?
                        $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" :
                        "";
                    fieldInvalidMessage = (fieldInvalidMessage == "" && !_stringLengthValidator(fieldValue, FirstNameMinLength, FirstNameMaxLength))
                        ? $"The length for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)} must be between {FirstNameMinLength} and {FirstNameMaxLength}{Environment.NewLine}"
                        : fieldInvalidMessage;
                    break;

                case FieldConstants.UserRegistrationField.LastName:
                    fieldInvalidMessage =
                        (!_requiredFieldValidator(fieldValue)) ?
                        $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" :
                        "";
                    fieldInvalidMessage = (fieldInvalidMessage == "" && !_stringLengthValidator(fieldValue, LastNameMinLength, LastNameMaxLength))
                        ? $"The length for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)} must be between {LastNameMinLength} and {LastNameMaxLength}{Environment.NewLine}"
                        : fieldInvalidMessage;
                    break;

                case FieldConstants.UserRegistrationField.Password:
                    fieldInvalidMessage =
                        (!_requiredFieldValidator(fieldValue)) ?
                        $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" :
                        "";
                    fieldInvalidMessage = (fieldInvalidMessage == "" && !_paternMatcherValidator(fieldValue, CommonRegularExpressionValidationPatterns.Strong_Password_RegEx_Pattern))
                        ? $"The password must be between 6 and 10 characters and contain at least one uppercase letter, one lowercase letter, one digit, and one special character{Environment.NewLine}"
                        : fieldInvalidMessage;
                    break;

                case FieldConstants.UserRegistrationField.PasswordCompare:
                    fieldInvalidMessage =
                        (!_requiredFieldValidator(fieldValue)) ?
                        $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" :
                        "";
                    fieldInvalidMessage = (fieldInvalidMessage == "" && !_compareFieldsValidator(fieldValue, fieldArray[(int)FieldConstants.UserRegistrationField.Password]))
                        ? $"The value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)} must be the same as the value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), FieldConstants.UserRegistrationField.Password)}{Environment.NewLine}"
                        : fieldInvalidMessage;
                    break;

                case FieldConstants.UserRegistrationField.DateOfBirth:
                    fieldInvalidMessage =
                        (!_requiredFieldValidator(fieldValue)) ?
                        $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" :
                        "";
                    fieldInvalidMessage = (fieldInvalidMessage == "" && !_dateOfBirthValidator(fieldValue, out DateTime validDateTime))
                        ? $"You must enter a valid date for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" 
                        :fieldInvalidMessage;
                    break;

                case FieldConstants.UserRegistrationField.PhoneNumber:
                    fieldInvalidMessage =
                        (!_requiredFieldValidator(fieldValue)) ?
                        $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" :
                        "";
                    fieldInvalidMessage = (fieldInvalidMessage == "" && !_paternMatcherValidator(fieldValue, CommonRegularExpressionValidationPatterns.My_Mobile_PhoneNumber_RegEx_Pattern))
                        ? $"You must enter a valid MY phone number{Environment.NewLine}"
                        : fieldInvalidMessage;
                    break;

                case FieldConstants.UserRegistrationField.AddressFirstLine:
                    fieldInvalidMessage =
                        (!_requiredFieldValidator(fieldValue)) ?
                        $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" :
                        "";
                    break;

                case FieldConstants.UserRegistrationField.AddressSecondLine:
                    fieldInvalidMessage =
                        (!_requiredFieldValidator(fieldValue)) ?
                        $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" :
                        "";
                    break;

                case FieldConstants.UserRegistrationField.AddressCity:
                    fieldInvalidMessage =
                        (!_requiredFieldValidator(fieldValue)) ?
                        $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" :
                        "";
                    break;

                case FieldConstants.UserRegistrationField.PostCode:
                    fieldInvalidMessage =
                        (!_requiredFieldValidator(fieldValue)) ?
                        $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" :
                        "";
                    fieldInvalidMessage = (fieldInvalidMessage == "" && !_paternMatcherValidator(fieldValue, CommonRegularExpressionValidationPatterns.My_Post_Code_RegEx_Pattern))
                        ? $"You did not enter a valid MY post code{Environment.NewLine}"
                        : fieldInvalidMessage;
                    break;

                default:
                    throw new ArgumentException($"This field does not exists");

            }
            return (fieldInvalidMessage == string.Empty);
        }

    }
}
