using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Utility {
    internal class HelperStatic {
        public static void takeScreenShot (IWebDriver driver, string name) { //Static screenshot method
            ITakesScreenshot ssdriver = driver as ITakesScreenshot;
            Screenshot screenshot = ssdriver.GetScreenshot();
            screenshot.SaveAsFile(@"C:\Screenshot\FinalProject\" + name + ".png", ScreenshotImageFormat.Png);
        }

        public static void takeScreenshotElm(IWebElement elm, string name) {
            ITakesScreenshot sselm = elm as ITakesScreenshot;
            Screenshot file = sselm.GetScreenshot();
            file.SaveAsFile(@"C:\Screenshot\FinalProject\" + name + ".png", ScreenshotImageFormat.Png);
        }
    }
}
