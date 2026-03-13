using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubMembershipApplication.FieldValidators
{
    public delegate bool FieldValidatorDelegate(
        int fieldIndex,
        string fieldValue,
        string[] fieldArray,
        out string fieldErrorMessage
        );
    internal interface IFieldValidator
    {
        void InitializeFieldValidatorsDelegates();
        string[] FieldArray { get; }
        FieldValidatorDelegate GetFieldValidator { get; }
    }
}
