using System;
using System.Collections.Generic;
using System.Text;

namespace Autotests
{
    public class User
    {
        public User(string name, string password)
        {
            Name = name;
            Password = password;
        }

        public User()
        {
            Name = "New user";
            Password = "New password";
        }

        public string Name { get; set; }
        public string Password { get; set; }
    }
}
