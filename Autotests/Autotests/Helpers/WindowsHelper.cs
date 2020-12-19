using System;
using System.Collections.Generic;
using System.Text;

namespace Autotests
{
    public class WindowsHelper : HelperBase
    {
        public WindowsHelper(ApplicationManager manager)
            : base(manager)
        {

        }

        public void GetWindowsSize()
        {
            driver.Manage().Window.Size = new System.Drawing.Size(1050, 708);
        }
    }
}
