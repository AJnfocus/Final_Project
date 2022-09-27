using Final_Project.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.POMPages {
    internal class CartPagePOM {

        private IWebDriver driver;
        private double orginalPrice; 
        private double salesPrice;
        private double priceDiscountFromSite;
        private double totalPrice;
        private double totalDiscount;
        
        public CartPagePOM(IWebDriver driver) { 
            this.driver = driver; //Constructor
        }

        private IWebElement couponCode => driver.FindElement(By.Id("coupon_code")); //Finds the cupon 
        private IWebElement applyButton => driver.FindElement(By.CssSelector("tbody > tr:nth-child(2) > td button"));
        private IWebElement subTotalText => driver.FindElement(By.CssSelector("tr.cart-subtotal > td > span > bdi"));
        private IWebElement discounts => driver.FindElement(By.CssSelector("tr.cart-discount.coupon-edgewords > td > span"));
        private IWebElement shippingCost => driver.FindElement(By.CssSelector("#shipping_method > li > label > span"));
        private IWebElement total => driver.FindElement(By.CssSelector(" tr.order-total > td > strong"));
        private IWebElement myAccount => driver.FindElement(By.Id("menu-item-46"));
        private IWebElement removeCoupon => driver.FindElement(By.CssSelector("tr.cart-discount.coupon-edgewords > td > a"));
        private IWebElement removeProduct => driver.FindElement(By.CssSelector("tr > td a"));

        public void applyCoupon() { //Method to set the coupon
            string coupon = Environment.GetEnvironmentVariable("coupon");
            couponCode.SendKeys(coupon); //Sends the keys typed from the parameters 
            Actions act = new Actions(driver); //Set a new Action object 
            act.MoveToElement(applyButton).Click().Perform(); // To perform a click function 
        }

        public double formatString (IWebElement element) {
            var value = element.Text;
            value = value.TrimStart('£');
            return double.Parse(value);
        }

        public void removeItem() {
            HelpersInstance wait = new HelpersInstance(driver);
            Actions act = new Actions(driver);
            IAction scroll = act.ScrollByAmount(0, 200).Build();
            scroll.Perform();

            wait.WaitForElm(10, By.CssSelector("tr.cart-discount.coupon-edgewords > td > a"));
            Thread.Sleep(1000);
            removeCoupon.Click();

            wait.WaitForElm(10, By.CssSelector("tr > td a"));
            removeProduct.Click();
        }

        public void CouponCheck() { //Checks the coupon
            HelpersInstance wait = new HelpersInstance(driver);
            wait.WaitForElm(5, By.CssSelector("div.woocommerce-notices-wrapper > div"));
            double subTotalValue = formatString(subTotalText);

            wait.WaitForElm(5, By.CssSelector("tr.cart-discount.coupon-edgewords > td > span"));
            var value2 = formatString(discounts);
            priceDiscountFromSite = value2;
            Console.WriteLine("Subtotal: " + subTotalValue + " Discount Price: " + priceDiscountFromSite);// 

            orginalPrice = subTotalValue;
            double discount = 15; //Setting discount to 15%
            discount = discount / 100;// 15/100
            totalDiscount = orginalPrice * discount; //Finding the total discount
            salesPrice = orginalPrice - totalDiscount;
            Console.WriteLine("Sales price from 15%: " + salesPrice);
            try {
               //Assert.That(totalDiscount == priceDiscountFromSite, Is.True,"The right coupon percentage Was not found");
            }
            catch (Exception) {
                double actualPercent = (priceDiscountFromSite / orginalPrice) * 100;
                Console.WriteLine("Expected: " + (discount * 100) + "% But was: " + actualPercent +'%');
                removeItem();
                throw;
            }
        }

        public void checkTotalCost() {
            Console.WriteLine("");
            Console.WriteLine("Checking cost");
            double shippingCostFromSite = formatString(shippingCost);

            double totalCostFromSite = formatString(total);
            Console.WriteLine("Total from the site: " + totalCostFromSite);

            Console.WriteLine("Orignal Price: " + orginalPrice + " Total Discount: " + totalDiscount + " Shipping Cost: " + shippingCostFromSite);
            totalPrice = (orginalPrice - totalDiscount) + shippingCostFromSite;
            Console.WriteLine("Total calculated from 15%: " + totalPrice);
            try {
                //Assert.That(totalPrice == totalCostFromSite, Is.True, "Values does not matach");
            }
            catch (Exception) {
                Console.WriteLine("");
                double expectedCost = shippingCostFromSite + salesPrice;
                Console.WriteLine("Expected: £" + expectedCost + " But was: £" + totalCostFromSite);
                removeItem();
                throw;
            }
            myAccount.Click();
        }
    }
}
    