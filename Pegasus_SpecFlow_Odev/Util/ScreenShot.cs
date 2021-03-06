﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pegasus_SpecFlow_Odev.Util
{
    public class ScreenShot
    {
        public IWebDriver Driver { get; set; }
        public ScreenShot(IWebDriver driver)
        {
            this.Driver = driver;
        }
        public void TakeScreenShot(string stepName)
        {
            Screenshot ss = ((ITakesScreenshot)Driver).GetScreenshot();
            string title = stepName;
            string Runname = title + DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss");
            string screenshotfilename = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin")) + @"Screenshoots/" + Runname + ".png";


            ss.SaveAsFile(screenshotfilename, ScreenshotImageFormat.Png);

        }
    }
}
