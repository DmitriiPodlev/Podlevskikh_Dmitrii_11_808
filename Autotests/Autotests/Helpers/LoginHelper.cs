using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Autotests
{
    public class LoginHelper : HelperBase
    {
        public LoginHelper(ApplicationManager manager)
            : base(manager)
        {

        }

        public bool IsLoggedIn()
        {
            return true;
        }

        public bool IsLoggedIn(string username)
        {
            return true;
        }


        public void Login(User user)
        {
            driver.FindElement(By.Id("login")).SendKeys(user.Name);
            driver.FindElement(By.Id("password")).Click();
            driver.FindElement(By.Id("password")).SendKeys(user.Password);
            driver.FindElement(By.CssSelector("tr:nth-child(4) > td:nth-child(1) > input")).Click();
        }

        public bool GetUser()
        {
            try
            {
                driver.FindElement(By.LinkText("Выход"));
            }
            catch { return false; }
            return true;
        }

        public void Logout()
        {
            driver.FindElement(By.LinkText("Выход")).Click();
        }

        public bool IsLogout()
        {
            try
            {
                driver.FindElement(By.CssSelector("body > table > tbody > tr:nth-child(1) > td:nth-child(1) > div:nth-child(1) > form > table > tbody > tr:nth-child(4) > td > input[type=submit]"));
            }
            catch { return false; }
            return true;
        }
    }
}
