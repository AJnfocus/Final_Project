using Final_Project.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Final_Project.Utility.HelperStatic;

namespace Final_Project.POMPages {
    internal class CartPagePOM {

        private IWebDriver driver;
        private double orginalPrice; //Gloabl fields which is used to store values 
        private double salesPrice;
        private double priceDiscountFromSite;
        private double totalPrice;
        private double totalDiscount;
        
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
        private IWebElement removeCoupon => driver.FindElement(By.CssSelector("tr.cart-discount.coupon-edgewords > td > a")); // Removes the coupon
        private IWebElement removeProduct => driver.FindElement(By.CssSelector("tr > td a")); // Removes the product

        public void applyCoupon() { //Method to set the coupon
            string coupon = Environment.GetEnvironmentVariable("coupon");//Sets the string with the local variable
            couponCode.SendKeys(coupon); //Sends the keys from variables  
            Console.WriteLine("\nApplying Coupon");
            Actions act = new Actions(driver); //Set a new Action object 
            act.MoveToElement(applyButton).Click().Perform(); // To perform a click function 
        }

        public double formatString (IWebElement element) { //Formats the text to show the double figure
            var value = element.Text;//Takes the IWebElement and converts to a string  
            value = value.TrimStart('£'); //Removes the £ char (£31 -> 31)
            return double.Parse(value); // Converts string to a double 
            //decimal 
        }

        public void removeItem() { // Removes the item and coupon
            HelpersInstance wait = new HelpersInstance(driver); //Create a helper class
            Actions act = new Actions(driver);
            IAction scroll = act.ScrollByAmount(0, 200).Build(); // Scroll to page to view remove coupon
            scroll.Perform();
            Console.WriteLine("\nRemoving product and coupon ");

            wait.WaitForElm(10, By.CssSelector("tr.cart-discount.coupon-edgewords > td > a"));
            Thread.Sleep(1000);
            removeCoupon.Click();// Removes the coupon

            wait.WaitForElm(10, By.CssSelector("tr > td a")); //Wait for the elem to be ready
            wait.takeScreenShot(driver, "RemoveItem");
            Thread.Sleep(1000);
            removeProduct.Click(); //Removes the product
        }

        public void CouponCheck() { //Checks the coupon
            HelpersInstance wait = new HelpersInstance(driver);
            wait.WaitForElm(5, By.CssSelector("div.woocommerce-notices-wrapper > div"));
            double subTotalValue = formatString(subTotalText); // Calls the convert method
            Console.WriteLine("Checking if coupon is 15% off ");

            wait.WaitForElm(5, By.CssSelector("tr.cart-discount.coupon-edgewords > td > span"));
            var value2 = formatString(discounts);
            priceDiscountFromSite = value2; //Stpre the values
            Console.WriteLine("Subtotal: £" + subTotalValue + " Discount Price: £" + priceDiscountFromSite);// 

            orginalPrice = subTotalValue;
            double discount = 15; //Setting discount to 15%
            discount = discount / 100;// 15/100
            totalDiscount = orginalPrice * discount; //Finding the total discount
            salesPrice = orginalPrice - totalDiscount;
            Console.WriteLine("Sales price from 15%: £" + salesPrice);
            wait.takeScreenShot(driver, "CouponCheck");
            try {
               Assert.That(totalDiscount == priceDiscountFromSite, Is.True,"The right coupon percentage Was not found");
            }
            catch (Exception) {
                double actualPercent = (priceDiscountFromSite / orginalPrice) * 100; //Find the actual percent using global variables
                Console.WriteLine("Expected: " + (discount * 100) + "% But was: " + actualPercent +'%');
                removeItem(); //Removes the item, easier for me to automate the test
                takeScreenShot(driver, "Removm Item Error");
                throw;
            }
        }

        public void checkTotalCost() {
            Console.WriteLine("");
            Console.WriteLine("Checking Cost");
            double shippingCostFromSite = formatString(shippingCost); //Calls the covert method

            double totalCostFromSite = formatString(total);
            Console.WriteLine("Total from the site: £" + totalCostFromSite);

            Console.WriteLine("Orignal Price: £" + orginalPrice + " Total Discount: £" + totalDiscount + " Shipping Cost: £" + shippingCostFromSite);
            totalPrice = (orginalPrice - totalDiscount) + shippingCostFromSite;
            Console.WriteLine("Total calculated from 15%: £" + totalPrice);
            try {
                Assert.That(totalPrice == totalCostFromSite, Is.True, "Values does not matach");
            }
            catch (Exception) {
                Console.WriteLine("");
                double expectedCost = shippingCostFromSite + salesPrice;//Finds the expectedCost from 15%
                Console.WriteLine("Expected: £" + expectedCost + " But was: £" + totalCostFromSite);
                removeItem();
                
                throw;
            }
            Thread.Sleep(2000);
            myAccount.Click();
        }
    }
}
    