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

        public void WaitForElm(int waitInSec, By locator) { //Explicit Wait for elm to display
            WebDriverWait myWait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitInSec));
            myWait.Until(drive => drive.FindElement(locator).Displayed);
        }

        public void takeScreenShot(IWebDriver driver ,string screenShotName) { //Screenshot method
            ITakesScreenshot ssdriver = driver as ITakesScreenshot;
            Screenshot screenshot = ssdriver.GetScreenshot();
            screenshot.SaveAsFile(@"C:\Screenshot\FinalProject\" + screenShotName +".png", ScreenshotImageFormat.Png);
        }
    }
}

