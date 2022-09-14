using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.POMPages {
    internal class CartPagePOM {
        private IWebDriver driver;
        public CartPagePOM(IWebDriver driver) { 
            this.driver = driver;
        }

        public IWebElement couponCode => driver.FindElement(By.Id("coupon_code"));
        public IWebElement applyButton => driver.FindElement(By.Name("apply_coupon"));

        public IWebElement subTotalText => driver.FindElement(By.CssSelector(
            "#post-5 > div > div > div.cart-collaterals > div > table > tbody > tr.cart-subtotal > td > span > bdi > span:first-child"));

        public void applyCoupon(string code) {
            couponCode.SendKeys(code);
            applyButton.Click();
        }

        public void CouponCheck() {
            Console.WriteLine(subTotalText.Text);
            //var orginalPrice = double.Parse(subTotalText.Text);
            //var dicount = double.Parse("15");
            //dicount = dicount / 100;
            //var amount = orginalPrice * dicount;
            //var salesPrice = orginalPrice - amount;
            //Console.WriteLine(salesPrice);
            //Assert.That(checkCoupon, Is.EqualTo(""),"Coupon Was not found");
        }
    }
}
