using Models;
using MvcWeb.Models.Promises;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcWeb.Models
{

    public class AuthUser : IAuthenticated
    {

        private User _user;

        public String Id
        {
            get
            {
                return _user?.Id;
            }
        }

        public String Email
        {
            get
            {
                return _user?.Email;
            }
        }

        public String FirstName
        {
            get
            {
                return _user?.FirstName;
            }
        }

        public String LastName
        {
            get
            {
                return _user?.LastName;
            }
        }

        public DateTime CreatedAt
        {
            get
            {
                return _user.CreatedAt.Value;
            }
        }

        public String AccessToken { private set; get; }

        public AuthUser(User user, String accessToken)
        {
            _user = user;
            AccessToken = accessToken;
        }

        public String getFullName()
        {
            return $"{FirstName} {LastName}";
        }
    }
}