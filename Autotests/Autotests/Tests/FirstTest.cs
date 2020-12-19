using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using NUnit.Framework;
using System.Xml.Serialization;
using System.IO;

namespace Autotests
{
    [TestFixture]
    public class FirstTest : TestBase
    {
        public static IEnumerable<User> UsersFromXmlFile()
        {
            return (List<User>)new XmlSerializer(typeof(List<User>))
                .Deserialize(new StreamReader(@"C:\Users\Dimon\source\repos\Autotests\AddressBookTestDataGenerators\bin\Release\netcoreapp3.1\users.xml"));
        }

        [Test, TestCaseSource("UsersFromXmlFile")]
        public void firstTest(User user)
        {
            app.Navigation.OpenHomePage();
            app.Windows.GetWindowsSize();
            app.Login.Login(user);
            var isLogin = app.Login.GetUser();
            Assert.AreEqual(true, isLogin);
        }
    }
}