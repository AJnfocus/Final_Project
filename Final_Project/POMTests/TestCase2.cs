using Final_Project.POMPages;
using Final_Project.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.POMTests {
    internal class TestCase2 : TestBaseClass {
        [Test]
        public void TestCase() {
            driver.Url = baseURL + "/my-account/";

            LoginPagePOM loginPage = new LoginPagePOM(driver);
            bool didWeLogin = loginPage.LoginWithValidCredentials(@"hello@mail.com", @"Password@<12345678");
            Assert.That(didWeLogin, Is.True, "We did not login");

            MyAccountPOM accountPOM = new MyAccountPOM(driver);
            accountPOM.shopButton();

            ShopPagePOM shopPagePOM = new ShopPagePOM(driver);
            shopPagePOM.getCap();

            ProductPagePOM productPagePOM = new ProductPagePOM(driver);
            productPagePOM.addToCart();

            CartPagePOM cartPagePOM = new CartPagePOM(driver);
            cartPagePOM.applyCoupon("edgewords");
            Thread.Sleep(2000);
            cartPagePOM.CouponCheck();
            Thread.Sleep(2000);

            CheckoutPagePOM checkoutPagePOM = new CheckoutPagePOM(driver);
            //driver.Url = "https://www.edgewordstraining.co.uk/demo-site/checkout/";
            Thread.Sleep(2000);
            checkoutPagePOM.checkout();
            Thread.Sleep(2000);

            int order = checkoutPagePOM.checkOrderNumber();
            Thread.Sleep(2000);

            int order2 = accountPOM.checkOrder();
            Assert.That(order == order2, Is.True, "Wrong Order Number Found");
            Thread.Sleep(5000);
            accountPOM.logout();

        }
    }
}
