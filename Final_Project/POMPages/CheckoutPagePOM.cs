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

        private IWebElement checkoutButton => driver.FindElement(By.LinkText("Proceed to checkout"));
        private IWebElement nameField => driver.FindElement(By.Id("billing_first_name"));
        private IWebElement surnameField => driver.FindElement(By.Id("billing_last_name"));
        private IWebElement houseStreet => driver.FindElement(By.Id("billing_address_1"));
        private IWebElement cityField => driver.FindElement(By.Id("billing_city"));
        private IWebElement postCodeField => driver.FindElement(By.Id("billing_postcode"));
        private IWebElement phoneField => driver.FindElement(By.Id("billing_phone"));
        //private IWebElement checkPaymentButton => driver.FindElement(By.CssSelector("li.wc_payment_method.payment_method_cod"));
        private IWebElement checkPaymentButton => driver.FindElement(By.CssSelector("li.wc_payment_method.payment_method_cheque"));
        private IWebElement placeOrderButton => driver.FindElement(By.Id("place_order"));
        private IWebElement getOrderNumber => driver.FindElement(By.CssSelector("div > .woocommerce ul li strong:nth-child(1)"));
        private IWebElement getDate => driver.FindElement(By.CssSelector("div > .woocommerce ul li:nth-child(2) strong"));

        public void checkout() { //Method to fill the fourm 
            
            HelpersInstance wait = new HelpersInstance(driver);
            Actions act = new Actions(driver);
            IAction scroll = act.ScrollByAmount(0, 500).Build();
            scroll.Perform();
            
            wait.WaitForElm(10, By.LinkText("Proceed to checkout"));
            checkoutButton.Click();

            wait.WaitForElm(5, By.Id("billing_first_name"));
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

            checkPaymentButton.Click();
            wait.WaitForElm(5, By.Id("place_order"));
            placeOrderButton.Click();
        }

        public int checkOrderNumber() { //Grabs the order number from confirmation page
            HelpersInstance wait = new HelpersInstance(driver);
            wait.WaitForElm(5, By.CssSelector("div > .woocommerce ul li strong:nth-child(1)"));
            orderNumber = int.Parse(getOrderNumber.Text); //Converts the text to int 
            date = getDate.Text;
            Console.WriteLine(orderNumber + " " + date); //Prints out to console 
            return orderNumber; // Returns the order number
        }

    }
}
