using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubMembershipApplication.Data
{
    internal interface IRegister
    {
        bool Register(string[] fields);

        bool EmailAddressExists(string emailAddress);

    }
}
