using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Pegasus_SpecFlow_Odev.Base;
using Pegasus_SpecFlow_Odev.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TechTalk.SpecFlow;

namespace Pegasus_SpecFlow_Odev.Steps
{
    [Binding]
    public sealed class Step1
    {
        ScreenShot screenshot;
        public BasePage basePage;
        public IWebDriver Driver { get; set; }
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly ScenarioContext context;

        public Step1(ScenarioContext injectedContext)
        {
            context = injectedContext;
        }

        [BeforeScenario]
        public void Setup()
        {
            Logging.Logger();
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("start-maximized");
            options.AddArgument("disable-popup-blocking");
            options.AddArgument("disable-notifications");
            options.AddArgument("test-type");
            Driver = new ChromeDriver(options);

            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            Driver.Navigate().GoToUrl("https://www.flypgs.com/");
            log.Info("Driver ayağa kalktı...");
            basePage = new BasePage(Driver);
            screenshot = new ScreenShot(Driver);
       
        }

        [BeforeStep]
        public void BeforStep()
        {
            log.Info("Step: " + context.StepContext.StepInfo.Text);
        }

        [Given("'(.*)' objesine tıklanır.")]
        public void SelectWhere(string obje)
        {
            log.Info("Parametre: " + obje);

            basePage.ClickElement(By.CssSelector(obje));
            //screenshot.TakeScreenShot(context.ScenarioInfo.Title);

        }
       

        [Given("'(.*)' alanına \'(.*)\' yazılır.")]
        public void SendKeys(string obje, string text)
        {
            log.Info("Parametre: " + obje);
            basePage.SendKeys(By.CssSelector(obje), text);
            //screenshot.TakeScreenShot(context.ScenarioInfo.Title);

        }

        [Given("'(.*)' seçimine '(.*)' tarihi yazılır.")]
        public void SelectDate(string text, string date)
        {
            string[] arr = date.Split("/");
            log.Info(text + " " + "Date: " + date);
            basePage.ChooseWay(text, arr);
            //screenshot.TakeScreenShot(context.ScenarioInfo.Title);

        }
        
        [Given("'(.*)' Bileti seçmek için '(.*)' tıklanır")]
        public void SelectTicket(string direction,string obje)
        {
            log.Info("Yön: "+ direction +"Parametre: " + obje);
            basePage.ClickElement(By.CssSelector(obje));
            

        }


        [Given("'(.*)' bilet tipini seçmek için '(.*)' tıklanır")]
        public void SelectTicketType(string type, string obje)
        {
            log.Info("Type: " + type + "Parametre: " + obje);
            basePage.ClickElement(By.CssSelector(obje));


        }

        [Given("Devam etmek için '(.*)' objesine tıklanır.")]
        public void PressButton(string obje)
        {
            log.Info("Parametre: " + obje);

            basePage.ClickElement(By.CssSelector(obje));
            //screenshot.TakeScreenShot(context.ScenarioInfo.Title);

        }

        [Given("'(.*)' saniye süreyle beklenir.")]
        public void TimeSeconds(int seconds)
        {
            log.Info("Parametre: " + seconds);

            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            //screenshot.TakeScreenShot(context.ScenarioInfo.Title);

        }



        [AfterScenario]
        public void TearDown()
        {
            Driver.Quit();
        }

    }
}
