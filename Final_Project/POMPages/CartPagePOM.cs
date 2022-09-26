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
        private IWebElement discount => driver.FindElement(By.CssSelector("tr.cart-discount.coupon-edgewords > td > span"));
        private IWebElement shippingCost => driver.FindElement(By.CssSelector("#shipping_method > li > label > span"));
        private IWebElement total => driver.FindElement(By.CssSelector(" tr.order-total > td > strong"));
        private IWebElement myAccount => driver.FindElement(By.Id("menu-item-46"));

        public void applyCoupon(string code) { //Method to set the coupon
            couponCode.SendKeys(code); //Sends the keys typed from the parameters 
            Actions act = new Actions(driver); //Set a new Action object 
            act.MoveToElement(applyButton).Click().Perform(); // To perform a click function 
        }

        public double formatString (IWebElement element) {
            var value = element.Text;
            value = value.TrimStart('£');
            return double.Parse(value);
        }

        public void CouponCheck() { //Checks the coupon
            double subTotalValue = formatString(subTotalText);

            var value2 = formatString(discount);
            priceDiscountFromSite = value2;
            Console.WriteLine("Subtotal: " + subTotalValue + " Discount Price: "+ priceDiscountFromSite);// 

            orginalPrice = subTotalValue;
            double dicount = 15; //Setting discount to 15%
            dicount = dicount / 100;// 15/100
            //var amount = orginalPrice * dicount;
            totalDiscount = orginalPrice * dicount; //Finding the total discount
            salesPrice = orginalPrice - totalDiscount;
            Console.WriteLine("Sales price from 15%: " + salesPrice);
            //Assert.That(totalDiscount == priceDiscountFromSite, Is.True,"Coupon Was not found");
        }

        public void checkTotalCost() {
            Console.WriteLine("");
            Console.WriteLine("Checking cost");
            double shippingCostFromSite = formatString(shippingCost);

            double totalCostFromSite = formatString(total);
            Console.WriteLine("Total from the site: " + totalCostFromSite);

            Console.WriteLine("Orignal Price: " + orginalPrice + " Total Discount: " + totalDiscount + " Shipping Cost: " + shippingCostFromSite);
            double totalCost = (orginalPrice - totalDiscount) + shippingCostFromSite;
            Console.WriteLine("Total calculated from 15%: " + totalCost);
            //Assert.That(totalCost == totalCostFromSite, Is.True, "Values does not matach");
            myAccount.Click();
        }
    }
}
    