using ClubMembershipApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubMembershipApplication.Data
{
    internal class LoginUser : ILogin
    {
        public User Login(string emailAddress, string password)
        {
            User user = null;

            using (var dbContext = new ClubMembershipDBContext())
            {
                user = dbContext.Users.FirstOrDefault
                    (u => u.EmailAddress.Trim().ToLower() == emailAddress.Trim().ToLower() 
                    && u.Password.Equals(password));
            }
            return user;
        }
    }
}
