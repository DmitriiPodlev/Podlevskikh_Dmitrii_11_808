using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Autotests
{
    public class HelperBase
    {
        protected IWebDriver driver;
        protected bool acceptNextAlert = true;
        protected ApplicationManager manager;

        public HelperBase(ApplicationManager manager)
        {
            this.manager = manager;
            this.driver = manager.Driver;
        }
    }
}
