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

        public CheckoutPagePOM setFirstName(string value) { //Makes a set method for each field 
            nameField.Clear();
            nameField.SendKeys(value);
            return this;
        }
        public CheckoutPagePOM setSurnameField(string value) {
            surnameField.Clear();
            surnameField.SendKeys(value);
            return this;
        }
        public CheckoutPagePOM setHouseStreet(string value) {
            houseStreet.Clear();
            houseStreet.SendKeys(value);
            return this;
        }
        public CheckoutPagePOM setCity(string value) {
            cityField.Clear();
            cityField.SendKeys(value);
            return this;
        }
        public CheckoutPagePOM setPostCode(string value) {
            postCodeField.Clear();
            postCodeField.SendKeys(value);
            return this;
        }
        public CheckoutPagePOM setPhone(string value) {
            phoneField.Clear();
            phoneField.SendKeys(value);
            return this;
        }
        public void checkout() { //Method to fill the fourm 
            Console.WriteLine("\nEntering user address  ");
            string name = Environment.GetEnvironmentVariable("firstName"); //Grabing info from local run setting
            string lastName = Environment.GetEnvironmentVariable("surname");
            string house = Environment.GetEnvironmentVariable("houseStreet");
            string city = Environment.GetEnvironmentVariable("city");
            string postcode = Environment.GetEnvironmentVariable("postcode");
            string phone = Environment.GetEnvironmentVariable("phone");

            HelpersInstance wait = new HelpersInstance(driver);
            Actions act = new Actions(driver);
            IAction scroll = act.ScrollByAmount(0, 500).Build(); //Action class to scroll down
            scroll.Perform();
            
            wait.WaitForElm(10, By.LinkText("Proceed to checkout")); //Wait until you see the button
            checkoutButton.Click();

            wait.WaitForElm(5, By.Id("billing_first_name"));
            setFirstName(name); //Calls the method above and pass through the runsetting variables 
            setSurnameField(lastName);
            setHouseStreet(house);
            setCity(city);
            setPostCode(postcode);
            setPhone(phone);


            checkPaymentButton.Click();
            wait.WaitForElm(5, By.Id("place_order"));
            //driver.Quit();
            placeOrderButton.Click();
        }

        public int checkOrderNumber() { //Grabs the order number from confirmation page
            HelpersInstance wait = new HelpersInstance(driver);
            wait.WaitForElm(5, By.CssSelector("div > .woocommerce ul li strong:nth-child(1)"));
            orderNumber = int.Parse(getOrderNumber.Text); //Converts the text to int 
            date = getDate.Text;
            Console.WriteLine(orderNumber + " " + date); //Prints out to console 
            wait.takeScreenShot(driver, "Order Number 1");
            return orderNumber; // Returns the order number
        }

    }
}
