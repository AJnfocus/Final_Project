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

        private IWebElement _currentPageAddToCart => driver.FindElement(By.ClassName("single_add_to_cart_button")); //Finds the "Add to Cart" button
        private IWebElement _viewCart => driver.FindElement(By.ClassName("cart-contents")); // Finds the Cart

        public void addToCart() {
            _currentPageAddToCart.Click(); //Clicks on the "Add to Cart"
            _viewCart.Click(); // Clicks on the cart 
        }

    }
}
