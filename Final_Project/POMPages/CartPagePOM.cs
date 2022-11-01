using Final_Project.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.POMPages {
    internal class CartPagePOM {

        private IWebDriver driver;
        
        public CartPagePOM(IWebDriver driver) { 
            this.driver = driver; //Constructor
        }

        private IWebElement _couponCode => driver.FindElement(By.Id("coupon_code")); //Finds the coupon 
        private IWebElement _applyButton => driver.FindElement(By.CssSelector("tbody > tr:nth-child(2) > td button"));//Finds the apply button
        private IWebElement _subTotalText => driver.FindElement(By.CssSelector("tr.cart-subtotal > td > span > bdi"));//Finds the subtotal
        private IWebElement _discounts => driver.FindElement(By.CssSelector("tr.cart-discount.coupon-edgewords > td > span"));// Finds the discount
        private IWebElement _shippingCost => driver.FindElement(By.CssSelector("#shipping_method > li > label > span")); //Finds the shipping
        private IWebElement _total => driver.FindElement(By.CssSelector(" tr.order-total > td > strong")); //Finds the total
        private IWebElement _myAccount => driver.FindElement(By.Id("menu-item-46"));//Find My Account button 
        private IWebElement _removeCoupon => driver.FindElement(By.CssSelector("tr.cart-discount.coupon-edgewords > td > a")); // Removes the coupon
        private IWebElement _removeProduct => driver.FindElement(By.CssSelector("tr > td a")); // Removes the product

        public void applyCoupon(string coupon) { 
            _couponCode.SendKeys(coupon); //Sends the keys from string parameter    
            Console.WriteLine("\nApplying Coupon " + coupon);
            _applyButton.Click();
        }

        public decimal formatString (IWebElement element) { //Formats the text to show the double figure
            var value = element.Text;//Takes the IWebElement and converts to a string  
            value = value.TrimStart('£'); //Removes the £ char (£31 -> 31)
            return decimal.Parse(value); // Converts string to a double 
        }

        public decimal obtainSubTotal() {
            HelpersInstance wait = new HelpersInstance(driver);
            wait.WaitForElm(5, By.CssSelector("div.woocommerce-notices-wrapper > div")); //Waits for subtotal to display
            return formatString(_subTotalText); // Calls the convert method
        }

        public decimal obtainDiscount() {
            HelpersInstance wait = new HelpersInstance(driver);
            wait.WaitForElm(5, By.CssSelector("tr.cart-discount.coupon-edgewords > td > span"));
            return formatString(_discounts);
        }

        public decimal obtainShippingCost() {
            return formatString(_shippingCost); 
        }

        public decimal obtainTotalCost() {
            return formatString(_total);
        }

        public void clickRemoveCoupon() {
            HelpersInstance wait = new HelpersInstance(driver);
            wait.WaitForElm(10, By.CssSelector("tr.cart-discount.coupon-edgewords > td > a"));
            _removeCoupon.Click();
        }

        public void clickRemoveProduct() {
            HelpersInstance wait = new HelpersInstance(driver);
            wait.WaitForElm(10, By.CssSelector("tr > td a")); //Wait for the elem to be ready
            _removeProduct.Click();
        }
    }
}
    