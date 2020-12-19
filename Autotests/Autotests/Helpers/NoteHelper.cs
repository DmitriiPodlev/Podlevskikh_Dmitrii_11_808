using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Autotests
{
    public class NoteHelper : HelperBase
    {
        public NoteHelper(ApplicationManager manager)
            : base(manager)
        {

        }

        public void CreateNote(Node node)
        {
            Thread.Sleep(3000);
            driver.FindElement(By.CssSelector("a:nth-child(1) > b")).Click();
            driver.FindElement(By.Id("add_data_text")).SendKeys(node.Text);
            driver.FindElement(By.Name("submit")).Click();
        }

        public bool GetNote()
        {
            try
            {
                driver.FindElement(By.CssSelector("#data_print23214 > table > tbody > tr > td:nth-child(2) > div"));
            }
            catch { return false; }
            return true;
        }

        public void EditNote(Node node)
        {
            Thread.Sleep(3000);
            driver.FindElement(By.LinkText("МЕНЮ")).Click();
            driver.FindElement(By.LinkText("Редактировать")).Click();
            driver.FindElement(By.Id("edit_data_text")).SendKeys(node.Text);
            driver.FindElement(By.CssSelector("#edit_data tr:nth-child(5) input")).Click();
        }

        public bool GetEditNote()
        {
            try
            {
                driver.FindElement(By.CssSelector("#data_print23211 > table > tbody > tr > td:nth-child(2) > div"));
            }
            catch { return false; }
            return true;
        }

        public void DeleteNote()
        {
            Thread.Sleep(3000);
            driver.FindElement(By.LinkText("МЕНЮ")).Click();
            driver.FindElement(By.LinkText("Удалить")).Click();
            Assert.That(driver.SwitchTo().Alert().Text, Is.EqualTo("Уверены что хотите удалить???(ID=23213)"));
        }

        public bool IsDeleted()
        {
            try { driver.FindElement(By.CssSelector("#data_print23200")); }
            catch { return true; }
            return false;
        }
    }
}
