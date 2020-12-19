using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Autotests
{
    public class ApplicationManager
    {
        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();
        private IWebDriver driver;
        public IDictionary<string, object> vars { get; private set; }
        private IJavaScriptExecutor js;

        private NavigationHelper navigation;
        private LoginHelper login;
        private WindowsHelper windows;
        private NoteHelper note;
        private string baseURL;

        private ApplicationManager()
        {
            driver = new ChromeDriver(@"D:\Спокойствие\chromedriver_win32");
            js = (IJavaScriptExecutor)driver;
            vars = new Dictionary<string, object>();
            baseURL = "http://pnote.ru/index.php";
            navigation = new NavigationHelper(this, baseURL);
            login = new LoginHelper(this);
            windows = new WindowsHelper(this);
            note = new NoteHelper(this);
        }

        public IWebDriver Driver
        {
            get
            {
                return driver;
            }
        }

        public NavigationHelper Navigation
        {
            get
            {
                return navigation;
            }
        }

        public LoginHelper Login
        {
            get
            {
                return login;
            }
        }

        public WindowsHelper Windows
        {
            get
            {
                return windows;
            }
        }

        public NoteHelper Note
        {
            get
            {
                return note;
            }
        }

        ~ApplicationManager()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                //ignore
            }
        }


        public static ApplicationManager GetInstance()
        {
            if (!app.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.Navigation.OpenHomePage();
                app.Value = newInstance;
            }
            return app.Value;
        }

    }
}
