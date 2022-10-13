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

        private IWebElement couponCode => driver.FindElement(By.Id("coupon_code")); //Finds the coupon 
        private IWebElement applyButton => driver.FindElement(By.CssSelector("tbody > tr:nth-child(2) > td button"));//Finds the apply button
        private IWebElement subTotalText => driver.FindElement(By.CssSelector("tr.cart-subtotal > td > span > bdi"));//Finds the subtotal
        private IWebElement discounts => driver.FindElement(By.CssSelector("tr.cart-discount.coupon-edgewords > td > span"));// Finds the discount
        private IWebElement shippingCost => driver.FindElement(By.CssSelector("#shipping_method > li > label > span")); //Finds the shipping
        private IWebElement total => driver.FindElement(By.CssSelector(" tr.order-total > td > strong")); //Finds the total
        private IWebElement myAccount => driver.FindElement(By.Id("menu-item-46"));//Find My Account button 
        public IWebElement removeCoupon => driver.FindElement(By.CssSelector("tr.cart-discount.coupon-edgewords > td > a")); // Removes the coupon
        public IWebElement removeProduct => driver.FindElement(By.CssSelector("tr > td a")); // Removes the product

        public void applyCoupon(string coupon) { 
            couponCode.SendKeys(coupon); //Sends the keys from variables  
            Console.WriteLine("\nApplying Coupon " + coupon);
            applyButton.Click();
        }

        public decimal formatString (IWebElement element) { //Formats the text to show the double figure
            var value = element.Text;//Takes the IWebElement and converts to a string  
            value = value.TrimStart('£'); //Removes the £ char (£31 -> 31)
            return decimal.Parse(value); // Converts string to a double 
        }

        public void removeItem() { // Removes the item and coupon if the percent or price is wrong
            HelpersInstance wait = new HelpersInstance(driver); //Create a helper class

            wait.ScrollPage(driver, 200);
            Console.WriteLine("\nRemoving product and coupon ");

            wait.WaitForElm(10, By.CssSelector("tr.cart-discount.coupon-edgewords > td > a"));
            Thread.Sleep(1000);
            removeCoupon.Click();// Removes the coupon

            wait.WaitForElm(10, By.CssSelector("tr > td a")); //Wait for the elem to be ready
            wait.takeScreenShot(driver, "RemoveItem");
            Thread.Sleep(1000);
            removeProduct.Click(); //Removes the product
        }

        public decimal obtainSubTotal() {
            HelpersInstance wait = new HelpersInstance(driver);
            wait.WaitForElm(5, By.CssSelector("div.woocommerce-notices-wrapper > div")); //Waits for subtotal to display
            return formatString(subTotalText); // Calls the convert method
        }

        public decimal obtainDiscount() {
            HelpersInstance wait = new HelpersInstance(driver);
            wait.WaitForElm(5, By.CssSelector("tr.cart-discount.coupon-edgewords > td > span"));
            return formatString(discounts);
        }

        public decimal obtainShippingCost() {
            return formatString(shippingCost); 
        }

        public decimal obtainTotalCost() {
            return formatString(total);
        }
    }
}
    