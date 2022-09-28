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
            bool didWeLogin = loginPage.LoginWithValidCredentials();
            Assert.That(didWeLogin, Is.True, "We did not login");

            MyAccountPOM accountPOM = new MyAccountPOM(driver);
            accountPOM.shopButton();

            ShopPagePOM shopPagePOM = new ShopPagePOM(driver);
            shopPagePOM.getCap();

            ProductPagePOM productPagePOM = new ProductPagePOM(driver);
            productPagePOM.addToCart();

            CartPagePOM cartPagePOM = new CartPagePOM(driver);
            cartPagePOM.applyCoupon();
            cartPagePOM.CouponCheck();
            Thread.Sleep(2000);

            CheckoutPagePOM checkoutPagePOM = new CheckoutPagePOM(driver);
            checkoutPagePOM.checkout();

            int order = checkoutPagePOM.checkOrderNumber(); //grabs order number from order summary 
            int order2 = accountPOM.checkOrder(); // Grabs order number from My Account orders tab
            Console.WriteLine("Expected: #" + order + " and found: #" + order2);
            try {
                Assert.That(order == order2, Is.True, "Wrong Order Number Found");
                //Checks if two order numbers matches
            }
            catch (Exception) {
                Console.WriteLine("Expected: #" + order + " But Found: #" + order2);
                //Error message if the wrong order number is found
                throw;
            }
            accountPOM.logout();
        }
    }
}
