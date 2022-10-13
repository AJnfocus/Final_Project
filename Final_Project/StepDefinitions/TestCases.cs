using Final_Project.POMPages;
using Final_Project.Utility;
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
        public int orderNumberFromSummary;

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
                username = row["Username"].ToString(); //Grabs the username from the inline table
                bool didWeLogin = loginPagePOM.LoginWithValidCredentials(username); //Passes the username to the login method
                Assert.That(didWeLogin, Is.True, "We logged in");
            }
            HelpersInstance help = new HelpersInstance(driver);
            NavBarPOM navBarPOM = new NavBarPOM(driver);
            help.WaitForElm(10, By.ClassName("woocommerce-store-notice")); //Dismiss the banner 
            navBarPOM.clickBanner();
        }

        [When(@"I add an item called '(.*)' into my basket")]
        public void WhenIAddAnItemCalledIntoMyBasket(string item) {
            NavBarPOM navBarPOM = new NavBarPOM(driver);
            navBarPOM.shopButton();

            ShopPagePOM shopPagePOM = new ShopPagePOM(driver);
            shopPagePOM.getItem(item); //The type of item is grabbed from the BDD

            ProductPagePOM productPagePOM = new ProductPagePOM(driver);
            productPagePOM.addToCart();
        }

        [When(@"apply a coupon '(.*)'")]
        public void WhenApplyACoupon(string coupon) {
            CartPagePOM cartPagePOM = new CartPagePOM(driver);
            cartPagePOM.applyCoupon(coupon);//Coupon code can be chagned from the BDD
        }

        [Then(@"it should apply a discount of ""(.*)""% off from the subtotal")]
        public void ThenItShouldApplyADiscountOfOffFromTheSubtotal(string p0) {
            CartPagePOM cartPagePOM = new CartPagePOM(driver);
            MyAccountPOM myAccountPOM = new MyAccountPOM(driver);
            NavBarPOM navBarPOM = new NavBarPOM(driver);
            HelpersInstance help = new HelpersInstance(driver);

            decimal subTotalFromSite = cartPagePOM.obtainSubTotal(); //Variables to hold values to later on compare
            decimal discountFromSite = cartPagePOM.obtainDiscount();
            decimal shippingCostFromSite = cartPagePOM.obtainShippingCost();
            decimal totalCostFromSite = cartPagePOM.obtainTotalCost();

            decimal discountValue = decimal.Parse(p0); // Can set the percentage from the BDD
            discountValue /= 100;
            decimal totalDiscount = subTotalFromSite * discountValue;
            decimal salesPrice = subTotalFromSite - totalDiscount;
            Console.WriteLine("Sales price from "  + p0 + "% £" + salesPrice);
            try {
                Assert.That(totalDiscount == discountFromSite, Is.True, "The right coupon percentage Was not found");
            }
            catch (Exception) {
                decimal actualPercent = (discountFromSite / subTotalFromSite) * 100;
                Console.WriteLine("Expected: " + p0 + "% But was: " + actualPercent + '%');
                cartPagePOM.removeItem();
                navBarPOM.accountButton();
                myAccountPOM.logout();
                throw;
            }

            Console.WriteLine("\nChecking Cost");
            Console.WriteLine("Total from the site: £" + totalCostFromSite);
            Console.WriteLine("Orignal Price: £" + subTotalFromSite + " Total Discount: £" + totalDiscount + " Shipping Cost: £" + shippingCostFromSite);
            decimal totalPrice = (subTotalFromSite - discountFromSite) + shippingCostFromSite;
            Console.WriteLine("Total Price: £" + totalPrice + " Total From Site: £" + totalCostFromSite);
            try {
                Assert.That(totalPrice == totalCostFromSite, Is.True, "Values does not matach");
            }
            catch (Exception) {
                decimal expectedCost = shippingCostFromSite + salesPrice;
                Console.WriteLine("Expected: £" + expectedCost + " But was: £" + totalCostFromSite);
                cartPagePOM.removeItem();
                navBarPOM.accountButton();
                myAccountPOM.logout();
                throw;
            }
        }

        [When(@"place an order with vaild billing details")]
        public void WhenPlaceAnOrderWithVaildBillingDetails(Table table) {
            CheckoutPagePOM checkoutPagePOM = new CheckoutPagePOM(driver);
            foreach (TableRow row in table.Rows) { // Goes through each row and gets the value and passes it to the checkout method
                checkoutPagePOM.checkout(
                    row["First Name"].ToString(),
                    row["Last Name"].ToString(), 
                    row["Street Address"].ToString(), 
                    row["Town"].ToString(), 
                    row["Postcode"].ToString(),
                    row["Phone"].ToString()
                    );
            }
            orderNumberFromSummary = checkoutPagePOM.checkOrderNumber(); //Grabs the order number from the summary
        }

        [Then(@"a order number would appear in the users account")]
        public void ThenAOrderNumberWouldAppearInTheUsersAccount() {
            MyAccountPOM myAccountPOM = new MyAccountPOM(driver);
            int orderNumberFromMyAcc = myAccountPOM.checkOrder(); //Grabs the order number from the summary and compares within My Account order number
            Assert.That(orderNumberFromSummary == orderNumberFromMyAcc, Is.True, "Wrong Order Number Found");
        }
    }
}
