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
        private IWebElement subTotalText => driver.FindElement(By.CssSelector(
            "#post-5 > div > div > div.cart-collaterals > div > table > tbody > tr.cart-subtotal > td > span > bdi"));
        private IWebElement discount => driver.FindElement(By.CssSelector(
            "#post-5 > div > div > div.cart-collaterals > div > table > tbody > tr.cart-discount.coupon-edgewords > td > span"));

        private IWebElement shippingCost => driver.FindElement(By.CssSelector(
            "#shipping_method > li > label > span"));

        private IWebElement total => driver.FindElement(By.CssSelector(
            "#post-5 > div > div > div.cart-collaterals > div > table > tbody > tr.order-total > td > strong > span"));

        private IWebElement myAccount => driver.FindElement(By.Id("menu-item-46"));

        public void applyCoupon(string code) { //Method to set the coupon
            couponCode.SendKeys(code); //Sends the keys typed from the parameters 
            Actions act = new Actions(driver); //Set a new Action object 
            act.MoveToElement(applyButton).Click().Perform(); // To perform a click function 
        }

        public void CouponCheck() { //Checks the coupon
            //Console.WriteLine(subTotalText.Text);
            var value = subTotalText.Text;  //Grabs the subtotal from table
            value = value.TrimStart('£'); //Remove the £
            //value = value.Substring(0, value.Length - 3);
            Console.WriteLine(value);
            
            var value2 = discount.Text;
            value2 = value2.TrimStart('£');
            priceDiscountFromSite = double.Parse(value2); //Converts the string to double and pass the value to the varaible
            Console.WriteLine("Subtotal: " + double.Parse(value) + " Discount Price: "+ priceDiscountFromSite);// 

            orginalPrice = double.Parse(value);
            double dicount = 15; //Setting discount to 15%
            dicount = dicount / 100;// 15/100
            //var amount = orginalPrice * dicount;
            totalDiscount = orginalPrice * dicount; //Finding the total discount
            salesPrice = orginalPrice - totalDiscount;
            Console.WriteLine(salesPrice);
            //Assert.That(totalDiscount == priceDiscountFromSite, Is.True,"Coupon Was not found");
        }

        public void checkTotalCost() {
            Console.WriteLine("checking cost");
            var valueFromSite = shippingCost.Text; //Grabs the shipping cost from the table 
            valueFromSite = valueFromSite.TrimStart('£');
            double shippingCostFromSite = double.Parse(valueFromSite);
            Console.WriteLine(shippingCostFromSite);

            var valueFromSite2 = total.Text; //Grabs the total cost from the table 
            valueFromSite2 = valueFromSite2.TrimStart('£');
            double totalCostFromSite = double.Parse(valueFromSite2);
            Console.WriteLine(totalCostFromSite);

            Console.WriteLine("Orignal Price: " + orginalPrice + " Total Discount: " + totalDiscount + " Shipping Cost: " + shippingCostFromSite);
            double totalCost = (orginalPrice - totalDiscount) + shippingCostFromSite;
            Console.WriteLine(totalCost);
            //Assert.That(totalCost == totalCostFromSite, Is.True, "Values does not matach");
            myAccount.Click();
        }
    }
}
    