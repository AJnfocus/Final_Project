using Final_Project.POMPages;
using Final_Project.Utility;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.POMTests {
    internal class LoginPage : TestBaseClass {
        [Test]
        public void Login() {
            driver.Url = baseURL + "/my-account/"; //Navigating to the login page 

            LoginPagePOM login = new LoginPagePOM(driver);
            bool didWeLogin = login.LoginWithValidCredentials(@"test@mail.com", @"HelloPassword123;");
            Assert.That(didWeLogin, Is.True, "We did not login");

            MyAccountPOM myAccountPOM = new MyAccountPOM(driver);
            myAccountPOM.shopButton();

            ShopPagePOM shade = new ShopPagePOM(driver);
            shade.getSunglasses();

            ProductPagePOM product = new ProductPagePOM(driver);
            product.addToCart();

            CartPagePOM checkout = new CartPagePOM(driver);
            checkout.applyCoupon("edgewords");
            Thread.Sleep(2000);
            checkout.CouponCheck();
            Thread.Sleep(2000);
            checkout.checkTotalCost();
            myAccountPOM.logout();
            Thread.Sleep(2000);
        }
    }
}
