using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareerCatalyst.Models
{
    public class UserMV
    {

        public UserMV()
        {
            Company = new CompanyMV();
        }
        public int UserID { get; set; }
        public int UserTypeID { get; set; }
        public string UserName { get; set; }
        public string password { get; set; }
        public string EmailAddress { get; set; }
        public string ContactNo { get; set; }

        public CompanyMV Company { get; set; }

    }
}