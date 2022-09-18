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
        private int orderNumber; //Field for order Number
        private string date; // Field for date 
        
        public CheckoutPagePOM(IWebDriver driver) { 
            this.driver = driver;
        }

        private IWebElement checkoutButton => driver.FindElement(By.CssSelector("#post-5 > div > div > div.cart-collaterals > div > div"));
        //private IWebElement checkoutButton => driver.FindElement(By.CssSelector(".alt.button.checkout-button.wc-forward"));

        private IWebElement nameField => driver.FindElement(By.Id("billing_first_name"));
        private IWebElement surnameField => driver.FindElement(By.Id("billing_last_name"));
        private IWebElement houseStreet => driver.FindElement(By.Id("billing_address_1"));
        private IWebElement cityField => driver.FindElement(By.Id("billing_city"));
        private IWebElement postCodeField => driver.FindElement(By.Id("billing_postcode"));
        private IWebElement phoneField => driver.FindElement(By.Id("billing_phone"));
        private IWebElement placeOrderButton => driver.FindElement(By.Id("place_order"));
        private IWebElement getOrderNumber => driver.FindElement(By.CssSelector("div > .woocommerce ul li strong:nth-child(1)"));
        private IWebElement getDate => driver.FindElement(By.CssSelector("div > .woocommerce ul li:nth-child(2) strong"));

        public void checkout() { //Method to fill the fourm 
            //checkoutButton.Click();
            //Actions act = new Actions(driver);
            //Thread.Sleep(2000);
            //act.MoveToElement(checkoutButton).Click().Perform();
            //Thread.Sleep(9000);
            //HelpersInstance wait = new HelpersInstance(driver);
            //wait.WaitForElm(10, By.CssSelector("#post-6 > header > h1"));
            nameField.Clear();
            surnameField.Clear();
            houseStreet.Clear();
            cityField.Clear();
            postCodeField.Clear();
            phoneField.Clear();

            nameField.SendKeys("Daz");
            surnameField.SendKeys("Bo");
            houseStreet.SendKeys("13 Happy Road");
            cityField.SendKeys("London");
            postCodeField.SendKeys("PA21 2BE");
            phoneField.SendKeys("07717278");

            placeOrderButton.Click();
        }

        public int checkOrderNumber() { //Grabs the order number from confirmation page
            orderNumber = int.Parse(getOrderNumber.Text); //Converts the text to int 
            date = getDate.Text;
            Console.WriteLine(orderNumber + " " + date); //Prints out to console 
            return orderNumber; // Returns the order number
        }

    }
}
