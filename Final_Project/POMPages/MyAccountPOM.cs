using Final_Project.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.POMPages {
    internal class MyAccountPOM {

        private IWebDriver driver;
        private int orderNumber; //An in to put the order number
        private string date; //String to put the date if needed 

        public MyAccountPOM(IWebDriver driver) { 
            this.driver = driver;
        }

        private IWebElement _shopNavButton => driver.FindElement(By.LinkText("Shop")); //Finds the Shop button
        private IWebElement _logoutButton => driver.FindElement(By.LinkText("Logout")); //Find the logout
        private IWebElement _myAccountButton => driver.FindElement(By.LinkText("My account")); //Find My account
        private IWebElement _ordersButton => driver.FindElement(By.CssSelector(
            "nav.woocommerce-MyAccount-navigation li:nth-child(2)")); //Finds the order button within My account
        private IWebElement _getOrderNumber => driver.FindElement(By.CssSelector("table tr td:nth-child(1) a")); //Finds the order number from table
        private IWebElement _getDate => driver.FindElement(By.CssSelector("table tr td:nth-child(2) time")); //Finds the date within the table
        private IWebElement _viewCart => driver.FindElement(By.ClassName("cart-contents"));

        public void logout() {
            HelpersInstance help = new HelpersInstance(driver);
            help.ScrollPage(driver, 300);
            _logoutButton.Click(); // Clicks on the logout button
        }

        public int checkOrder() { //Returns the order number from My Account
            _myAccountButton.Click(); //Clicks on the My Account once we the order has been confirmed 
            _ordersButton.Click(); //Clicks on the order 
            HelpersInstance wait = new HelpersInstance(driver);
            wait.WaitForElm(5, By.CssSelector("table tr td:nth-child(1) a"));
            wait.takeScreenShot(driver, "Order Number 2");
            string convert = _getOrderNumber.Text; //Grab the text from IWebElement 
            convert = convert.Trim('#'); //Trims the # in front of the order 
            orderNumber = int.Parse(convert); // Converts it to an int
            return orderNumber; 
        }
    }
}
