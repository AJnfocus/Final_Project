using Final_Project.POMPages;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using static Final_Project.Utility.Hook;

namespace Final_Project.StepDefinitions {

    [Binding]
    public class TestCases {

        public string baseURL = "https://www.edgewordstraining.co.uk";
        public int order1;

        [Given(@"When I am in the login page")]
        public void GivenWhenIAmInTheLoginPage() {
            driver.Url = baseURL + "/demo-site/my-account/";
        }

        [Given(@"I provide my login details")]
        public void GivenIProvideMyLoginDetails(Table table) {
            string username;
            string password;
            LoginPagePOM loginPagePOM = new LoginPagePOM(driver);

            foreach (TableRow row in table.Rows) {
                username = row["Username"].ToString();
                password = row["Password"].ToString();
                //Console.WriteLine(username + " " + password);
                bool didWeLogin = loginPagePOM.LoginWithValidCredentials();
                Assert.That(didWeLogin, Is.True, "We did not login");
            }
        }

        [When(@"I add an item called '(.*)' into my basket")]
        public void WhenIAddAnItemCalledIntoMyBasket(string item) {
            MyAccountPOM accountPOM = new MyAccountPOM(driver);
            accountPOM.shopButton();

            ShopPagePOM shopPagePOM = new ShopPagePOM(driver);
            shopPagePOM.getSunglasses();

            ProductPagePOM productPagePOM = new ProductPagePOM(driver);
            productPagePOM.addToCart();
        }

        [When(@"apply a coupon '(.*)'")]
        public void WhenApplyACoupon(string edgeword0) {
            CartPagePOM cartPagePOM = new CartPagePOM(driver);
            cartPagePOM.applyCoupon();
        }

        [Then(@"it should apply a discount of ""(.*)""% off from the subtotal")]
        public void ThenItShouldApplyADiscountOfOffFromTheSubtotal(string p0) {
            CartPagePOM cartPagePOM = new CartPagePOM(driver);
            Thread.Sleep(2000);
            cartPagePOM.CouponCheck();
            Thread.Sleep(2000);
            cartPagePOM.checkTotalCost();
            Thread.Sleep(2000);
            MyAccountPOM myAccountPOM = new MyAccountPOM(driver);
            cartPagePOM.removeItem();
            myAccountPOM.logout();
        }

        [When(@"place an order with vaild billing details")]
        public void WhenPlaceAnOrderWithVaildBillingDetails(Table table) {
            CheckoutPagePOM checkoutPagePOM = new CheckoutPagePOM(driver);
            checkoutPagePOM.checkout();
            Thread.Sleep(2000);
            order1 = checkoutPagePOM.checkOrderNumber();
            Thread.Sleep(2000);
        }

        [Then(@"a order number would appear in the users account")]
        public void ThenAOrderNumberWouldAppearInTheUsersAccount() {
            MyAccountPOM myAccountPOM = new MyAccountPOM(driver);
            int order2 = myAccountPOM.checkOrder();
            Assert.That(order1 == order2, Is.True, "Wrong Order Number Found");
            Thread.Sleep(5000);
            myAccountPOM.logout();
        }
    }
}
