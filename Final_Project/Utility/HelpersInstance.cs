using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Interactions;
using Final_Project.POMPages;

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

        public void ScrollPage(IWebDriver driver, int amount) {
            Actions act = new Actions(driver);
            IAction scroll = act.ScrollByAmount(0, amount).Build(); // Scroll to page to view remove coupon
            scroll.Perform();
        }

        public void removeItems(IWebDriver driver) {
            CartPagePOM cartPagePOM = new CartPagePOM(driver);
            NavBarPOM navBarPOM = new NavBarPOM(driver);

            navBarPOM.cartButton();
            Console.WriteLine("\nRemoving product and coupon");
            ScrollPage(driver, 200);

            cartPagePOM.clickRemoveCoupon();

            takeScreenShot(driver, "RemoveItem");
            Thread.Sleep(1000);
            cartPagePOM.clickRemoveProduct();
        }
    }
}

