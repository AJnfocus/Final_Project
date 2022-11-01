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

        private IWebElement _shopNavButton => driver.FindElement(By.LinkText("Shop"));
        private IWebElement _myAccountButton => driver.FindElement(By.LinkText("My account"));
        private IWebElement _viewCart => driver.FindElement(By.ClassName("cart-contents")); // Finds the Cart
        private IWebElement _emptyCart => driver.FindElement(By.CssSelector("ul > li > a > span"));
        private IWebElement _dismissBanner => driver.FindElement(By.LinkText("Dismiss"));

        public void accountButton() {
            HelpersInstance help = new HelpersInstance(driver);
            help.ScrollPage(driver, -800);
            _myAccountButton.Click(); //Clicks on the my account button
        }
        public void shopButton() {
            _shopNavButton.Click(); //Clicks on the shop button the nav bar
        }
        public void cartButton() {
            _viewCart.Click();
        }
        public void clickBanner() {
            _dismissBanner.Click();
        }

        public string getCartValue() {
            return _emptyCart.Text;
        }
    }
}
