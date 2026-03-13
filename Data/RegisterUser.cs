using ClubMembershipApplication.FieldValidators;
using ClubMembershipApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubMembershipApplication.Data
{
    internal class RegisterUser : IRegister
    {
        // This method checks if a given email address already exists in the database by querying the Users table using Entity Framework.
        public bool EmailAddressExists(string emailAddress)
        {
            bool emailAddressExists = false;

            using (var dbContext = new ClubMembershipDBContext())
            {
                emailAddressExists = dbContext.Users.Any(u => u.EmailAddress.Trim().ToLower() == emailAddress.Trim().ToLower());
            }

            return emailAddressExists;
        }

        // This method takes an array of strings representing user registration fields,
        // creates a new User object, and saves it to the database using Entity Framework.
        public bool Register(string[] fields)
        {
            using (var dbContext = new ClubMembershipDBContext())
            {
                var user = new User
                {
                    EmailAddress = fields[(int)FieldConstants.UserRegistrationField.EmailAddress],
                    FirstName = fields[(int)FieldConstants.UserRegistrationField.FirstName],
                    LastName = fields[(int)FieldConstants.UserRegistrationField.LastName],
                    Password = fields[(int)FieldConstants.UserRegistrationField.Password],
                    DateOfBirth = DateTime.Parse(fields[(int)FieldConstants.UserRegistrationField.DateOfBirth]),
                    PhoneNumber = fields[(int)FieldConstants.UserRegistrationField.PhoneNumber],
                    AddressFirstLine= fields[(int)FieldConstants.UserRegistrationField.AddressFirstLine],
                    AddressSecondLine = fields[(int)FieldConstants.UserRegistrationField.AddressSecondLine],
                    AddressCity = fields[(int)FieldConstants.UserRegistrationField.AddressCity],
                    PostCode = fields[(int)FieldConstants.UserRegistrationField.PostCode]
                };
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
            }
            return true;
        }
    }
}
