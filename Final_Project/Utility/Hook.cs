using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Final_Project.Utility {
    [Binding]
    internal class Hook {

        public static IWebDriver driver;

        [BeforeScenario]
        public void setup() {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [AfterScenario]
        public void teardown() {
            Thread.Sleep(2000);
            driver.Quit();
        }

    }
}
