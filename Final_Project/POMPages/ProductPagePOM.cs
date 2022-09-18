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
            this.driver = driver; //The constructor 
        }

        public IWebElement currentPageAddToCart =>
            driver.FindElement(By.ClassName("single_add_to_cart_button")); //Finds the "Add to Cart" button
        public IWebElement viewCart => driver.FindElement(By.ClassName("cart-contents")); // Finds the Cart

        public void addToCart() {
            currentPageAddToCart.Click(); //Clicks on the "Add to Cart"
            viewCart.Click(); // Clicks on the cart 
        }
    }
}
