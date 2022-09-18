using OpenQA.Selenium;
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

        private IWebElement shopNavButton => driver.FindElement(By.LinkText("Shop")); //Finds the Shop button
        private IWebElement logoutButton => driver.FindElement(By.LinkText("Logout")); //Find the logout
        private IWebElement myAccountButton => driver.FindElement(By.LinkText("My account")); //Find My account
        private IWebElement ordersButton => driver.FindElement(By.CssSelector(
            "nav.woocommerce-MyAccount-navigation li:nth-child(2)")); //Finds the order button within My account
        private IWebElement getOrderNumber => driver.FindElement(By.CssSelector("table tr td:nth-child(1) a")); //Finds the order number from table
        private IWebElement getDate => driver.FindElement(By.CssSelector("table tr td:nth-child(2) time")); //Finds the date within the table

        public void shopButton() {
            shopNavButton.Click(); //Clicks on the shop button the nav bar
        }

        public void logout() {
            logoutButton.Click(); // Clicks on the logout button
        }

        public int checkOrder() { //Returns the order number from My Account
            myAccountButton.Click(); //Clicks on the My Account once we the order has been confirmed 
            ordersButton.Click(); //Clicks on the order 
            string convert = getOrderNumber.Text; //Grab the text from IWebElement 
            convert = convert.Trim('#'); //Trims the # in front of the order 
            orderNumber = int.Parse(convert); // Converts it to an int
            return orderNumber; 
        }
    }
}
