using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Pegasus_SpecFlow_Odev.Util;

namespace Pegasus_SpecFlow_Odev.Base
{
    public class BasePage
    {
        IReadOnlyList<IWebElement> days;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        IWebDriver driver;
        WebDriverWait wait;
        IJavaScriptExecutor scriptExecutor;
        string gidisYil =" #search-flight-datepicker-departure > div > div.ui-datepicker-group.ui-datepicker-group-first > div > div > span.ui-datepicker-year" ;
        string nextButtonGidis = "//*[@id='search-flight-datepicker-departure']/div/div[2]/div/a";
        string gidisAy = "#search-flight-datepicker-departure > div > div.ui-datepicker-group.ui-datepicker-group-first > div > div > span.ui-datepicker-month";
        string gidisGun = "//*[@id='search-flight-datepicker-departure']/div/div[1]//tbody//a";

        string nextButtonDonus = "//*[@id='search-flight-datepicker-arrival']/div/div[2]/div/a";
       
        string donusYil= "//*[@id='search-flight-datepicker-arrival']/div/div[1]/div/div/span[2]";
        string donusAy = "//*[@id='search-flight-datepicker-arrival']/div/div[2]/div/div/span[1]";
        //string donusGun = "//*[@id='search-flight-datepicker-departure']/div/div[1]//tbody//a";
        string donusGun = "//*[@id='search-flight-datepicker-arrival']/div/div[2]/table/tbody//a";
        public BasePage(IWebDriver driver)
        {


            this.driver = driver;

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
      
        public void ChooseWay(string text, string[] arr)
        {
            if (text.Equals("Gidis")) 
            {
                ChooseYear(arr[2],"Gidis");
                ChooseMonth(arr[1],"Gidis");
                ChooseDay(arr[0],"Gidis");
            }
            else if(text.Equals("Donus"))
            {
                ChooseYear(arr[2], "Donus");
                ChooseMonth(arr[1], "Donus");
                ChooseDay(arr[0], "Donus");

            }
        }

        public void ChooseYear(string year,string text)
        {
            string yil;
            while (true)
            {
                if (text=="Gidis")
                {
                    yil = FindElement(By.CssSelector(gidisYil)).Text;
                    if (yil.Equals(year))
                        break;
               

                    ClickElement(By.XPath(nextButtonGidis));
                }
              else if (text == "Donus")
                {
                      yil = FindElement(By.XPath(donusYil)).Text;
                if (yil.Equals(year))
                     break;
    

              ClickElement(By.XPath(nextButtonDonus));
                }

            }
        }

        public void ChooseMonth(string month,string text)
        {
            string ay;
            while (true)
            {
                if (text == "Gidis")
                {
                    ay = FindElement(By.CssSelector(gidisAy)).Text;
                    if (ay.Equals(month))
                        break;
                  
                    ClickElement(By.XPath(nextButtonGidis));
                }
                else if (text == "Donus")
                {
                    ay = FindElement(By.XPath(donusAy)).Text;
                    if (ay.Equals(month))
                        break;
                  
                    ClickElement(By.XPath(nextButtonDonus));
                }
               

            }
        }

        public void ChooseDay(string day, string text)
        {
           
            if (text=="Gidis")
            {
                days = driver.FindElements(By.XPath(gidisGun));
                foreach (var gun in days)
                {
                    if (gun.Text.Equals(day))
                    {
                        gun.Click();
                        break;
                    }
                }
            }
            else if (text=="Donus")
            {
                days = driver.FindElements(By.XPath(donusGun));
                foreach (var gun in days)
                {
                    if (gun.Text.Equals(day))
                    {
                        gun.Click();
                        break;
                    }
                }
            }
           
           
        }


        public IWebElement FindElement(By by)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(by));
            HightLightElement(by);


            return driver.FindElement(by);
        }

        public void ClickElement(By by)
        {
            FindElement(by).Click();
        }

        public SelectElement SelectOptions(IWebElement element)
        {
            return new SelectElement(element);
        }

        public void SendKeys(By by, String value)
        {
            FindElement(by).SendKeys(value);
        }
        public void SelectElementByText(By by, String visibleText)
        {

            SelectOptions(FindElement(by)).SelectByText(visibleText);

        }

        public void HoverElement(By by)
        {


            Actions actions = new Actions(driver);
            actions.MoveToElement(FindElement(by)).Build().Perform();

        }

        public string GetText(By by)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
            string elementText = FindElement(by).Text;
            log.Info("Element Text :" + elementText);
            return elementText;
        }
        public void HightLightElement(By by)
        {
            scriptExecutor = (IJavaScriptExecutor)driver;
            scriptExecutor.ExecuteScript("arguments[0].setAttribute('style', 'background: yellow; border: 2px solid red;');", driver.FindElement(by));
            Thread.Sleep(TimeSpan.FromMilliseconds(700));
        }

        public Boolean MatchText(By by, string expected)
        {
            if (GetText(by).Equals(expected))
            {
                return true;

            }
            else
            {
                log.Warn("Değerler birbiri ile eşleşmiyor!!!!!!!");
            }
            return false;

        }
    }
}
