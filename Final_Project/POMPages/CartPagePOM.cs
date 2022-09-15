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
            this.driver = driver;
        }

        private IWebElement couponCode => driver.FindElement(By.Id("coupon_code"));
        //private IWebElement applyButton => driver.FindElement(By.Name("apply_coupon"));
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

        public void applyCoupon(string code) {
            couponCode.SendKeys(code);
            Actions act = new Actions(driver);
            act.MoveToElement(applyButton).Click().Perform();

            //applyButton.Click();
        }

        public void CouponCheck() {
            //Console.WriteLine(subTotalText.Text);
            var value = subTotalText.Text;
            value = value.TrimStart('£');
            //value = value.Substring(0, value.Length - 3);
            Console.WriteLine(value);
            var value2 = discount.Text;
            value2 = value2.TrimStart('£');
            priceDiscountFromSite = double.Parse(value2);
            Console.WriteLine(double.Parse(value) + " "+ priceDiscountFromSite);

            orginalPrice = double.Parse(value);
            double dicount = 15;
            dicount = dicount / 100;
            //var amount = orginalPrice * dicount;
            totalDiscount = orginalPrice * dicount;
            salesPrice = orginalPrice - totalDiscount;
            Console.WriteLine(salesPrice);
            //Assert.That(totalDiscount == priceDiscountFromSite, Is.True,"Coupon Was not found");
        }

        public void checkTotalCost() {
            Console.WriteLine("checking cost");
            var valueFromSite = shippingCost.Text;
            valueFromSite = valueFromSite.TrimStart('£');
            double shippingCost1 = double.Parse(valueFromSite);
            Console.WriteLine(shippingCost1);

            var valueFromSite2 = total.Text;
            valueFromSite2 = valueFromSite2.TrimStart('£');
            double totalCostFromSite = double.Parse(valueFromSite2);
            Console.WriteLine(totalCostFromSite);

            Console.WriteLine(orginalPrice + " " + totalDiscount + " " + shippingCost1);
            double totalCost = (orginalPrice - totalDiscount) + shippingCost1;
            Console.WriteLine(totalCost);
            //Assert.That(totalCost == totalCostFromSite, Is.True, "Values does not matach");
            myAccount.Click();
        }
    }
}
    