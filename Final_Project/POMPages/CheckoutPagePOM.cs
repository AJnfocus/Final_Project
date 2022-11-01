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

        private IWebElement _checkoutButton => driver.FindElement(By.LinkText("Proceed to checkout"));
        private IWebElement _nameField => driver.FindElement(By.Id("billing_first_name"));
        private IWebElement _surnameField => driver.FindElement(By.Id("billing_last_name"));
        private IWebElement _houseStreet => driver.FindElement(By.Id("billing_address_1"));
        private IWebElement _cityField => driver.FindElement(By.Id("billing_city"));
        private IWebElement _postCodeField => driver.FindElement(By.Id("billing_postcode"));
        private IWebElement _phoneField => driver.FindElement(By.Id("billing_phone"));
        private IWebElement _checkPaymentButton => driver.FindElement(By.CssSelector("li.wc_payment_method.payment_method_cheque"));
        private IWebElement _placeOrderButton => driver.FindElement(By.Id("place_order"));
        private IWebElement _getOrderNumber => driver.FindElement(By.CssSelector("div > .woocommerce ul li strong:nth-child(1)"));
        private IWebElement _getDate => driver.FindElement(By.CssSelector("div > .woocommerce ul li:nth-child(2) strong"));

        public CheckoutPagePOM setFirstName(string value) { //Makes a set method for each field 
            _nameField.Clear();
            _nameField.SendKeys(value);
            return this;
        }
        public CheckoutPagePOM setSurnameField(string value) {
            _surnameField.Clear();
            _surnameField.SendKeys(value);
            return this;
        }
        public CheckoutPagePOM setHouseStreet(string value) {
            _houseStreet.Clear();
            _houseStreet.SendKeys(value);
            return this;
        }
        public CheckoutPagePOM setCity(string value) {
            _cityField.Clear();
            _cityField.SendKeys(value);
            return this;
        }
        public CheckoutPagePOM setPostCode(string value) {
            _postCodeField.Clear();
            _postCodeField.SendKeys(value);
            return this;
        }
        public CheckoutPagePOM setPhone(string value) {
            _phoneField.Clear();
            _phoneField.SendKeys(value);
            return this;
        }
    
        public void checkout(string name, string lastName, string house, string city, string postcode, string phone) { //Method to fill the fourm 
            Console.WriteLine("\nEntering user address  ");
            
            HelpersInstance wait = new HelpersInstance(driver);
            wait.ScrollPage(driver, 800); //Scrolls to bottom of the cart page 
            wait.WaitForElm(10, By.LinkText("Proceed to checkout")); //Wait until you see the button
            _checkoutButton.Click();

            wait.WaitForElm(5, By.Id("billing_first_name"));
            setFirstName(name); //Calls the method above and pass through the runsetting variables 
            setSurnameField(lastName);
            setHouseStreet(house);
            setCity(city);
            setPostCode(postcode);
            setPhone(phone);

            Thread.Sleep(1000);
            _checkPaymentButton.Click();
            wait.WaitForElm(5, By.Id("place_order"));
            //driver.Quit();
            _placeOrderButton.Click();
        }

        public int checkOrderNumber() { //Grabs the order number from confirmation page
            HelpersInstance wait = new HelpersInstance(driver);
            wait.WaitForElm(5, By.CssSelector("div > .woocommerce ul li strong:nth-child(1)"));
            orderNumber = int.Parse(_getOrderNumber.Text); //Converts the text to int 
            date = _getDate.Text;
            Console.WriteLine(orderNumber + " " + date); //Prints out to console 
            wait.takeScreenShot(driver, "Order Number 1");
            return orderNumber; // Returns the order number
        }

    }
}
