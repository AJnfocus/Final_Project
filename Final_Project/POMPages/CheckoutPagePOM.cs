using Final_Project.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.POMPages {
    internal class CheckoutPagePOM {
        
        private IWebDriver driver;
        
        public CheckoutPagePOM(IWebDriver driver) { 
            this.driver = driver;
        }

        private IWebElement checkoutButton => driver.FindElement(By.CssSelector("#post-5 > div > div > div.cart-collaterals > div > div"));
        private IWebElement nameField => driver.FindElement(By.Id("billing_first_name"));
        private IWebElement surnameField => driver.FindElement(By.Id("billing_last_name"));
        private IWebElement houseStreet => driver.FindElement(By.Id("billing_address_1"));
        private IWebElement cityField => driver.FindElement(By.Id("billing_city"));
        private IWebElement postCodeField => driver.FindElement(By.Id("billing_postcode"));
        private IWebElement phoneField => driver.FindElement(By.Id("billing_phone"));

        public void checkout() {
            //checkoutButton.Click();
            //Actions act = new Actions(driver);
            //Thread.Sleep(2000);
            //act.MoveToElement(checkoutButton).Click().Perform();
            //Thread.Sleep(9000);
            //HelpersInstance wait = new HelpersInstance(driver);
            //wait.WaitForElm(10, By.CssSelector("#post-6 > header > h1"));

            nameField.SendKeys("H");
            surnameField.SendKeys("S");
            houseStreet.SendKeys("A");
            cityField.SendKeys("C");
            postCodeField.SendKeys("PA21 2BE");
            phoneField.SendKeys("011");
        }


    }
}
