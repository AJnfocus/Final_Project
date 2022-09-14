using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Utility {
    internal class HelpersInstance {

        IWebDriver driver;
        public HelpersInstance(IWebDriver driver) {
            this.driver = driver;
        }

        public void WaitForElm(int waitInSec, By locator) {
            WebDriverWait myWait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitInSec));
            myWait.Until(drive => drive.FindElement(locator).Displayed);
        }

    }

}

