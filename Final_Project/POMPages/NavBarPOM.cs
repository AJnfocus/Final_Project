using Final_Project.Utility;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.POMPages {
    internal class NavBarPOM {
        private IWebDriver driver;
        public NavBarPOM(IWebDriver driver) {
            this.driver = driver; // the constructor
        }

        private IWebElement shopNavButton => driver.FindElement(By.LinkText("Shop"));
        private IWebElement myAccountButton => driver.FindElement(By.LinkText("My account"));
        private IWebElement viewCart => driver.FindElement(By.ClassName("cart-contents")); // Finds the Cart
        public IWebElement emptyCart => driver.FindElement(By.CssSelector("ul > li > a > span"));
        private IWebElement dismissBanner => driver.FindElement(By.LinkText("Dismiss"));

        public void accountButton() {
            HelpersInstance help = new HelpersInstance(driver);
            help.ScrollPage(driver, -800);
            myAccountButton.Click(); //Clicks on the my account button
        }
        public void shopButton() {
            shopNavButton.Click(); //Clicks on the shop button the nav bar
        }
        public void cartButton() {
            viewCart.Click();
        }
        public void clickBanner() {
            dismissBanner.Click();
        }
    }
}
