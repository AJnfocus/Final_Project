using Final_Project.Utility;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.POMPages {
    internal class ProductPagePOM {
        private IWebDriver driver;

        public ProductPagePOM(IWebDriver driver) { 
            this.driver = driver;
        }

        public IWebElement currentPage =>
            driver.FindElement(By.ClassName("single_add_to_cart_button"));
        public IWebElement viewCart => driver.FindElement(By.ClassName("cart-contents"));

        public void addToCart() {
            currentPage.Click();
            viewCart.Click();
        }
    }
}
