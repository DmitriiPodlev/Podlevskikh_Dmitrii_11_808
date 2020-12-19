﻿using System;
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

namespace Autotests
{
    public class TestBase
    {

        public User user = new User("dimasik33", "dimasik33junior");

        protected ApplicationManager app;
        [SetUp]
        public void SetUp()
        {
            app = ApplicationManager.GetInstance();
        }
    }
}