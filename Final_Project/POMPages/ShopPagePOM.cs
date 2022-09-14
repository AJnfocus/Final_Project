using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.POMPages {
    internal class ShopPagePOM {
        private IWebDriver driver;
        public ShopPagePOM(IWebDriver driver) { 
            this.driver = driver; 
        }

        public IWebElement sunglass =>
            driver.FindElement(By.CssSelector("#main > ul > li.product.type-product.post-30.status-publish.first.instock.product_cat-accessories.has-post-thumbnail.featured.shipping-taxable.purchasable.product-type-simple > a.woocommerce-LoopProduct-link.woocommerce-loop-product__link"));
        public void getSunglasses() {
            sunglass.Click();
        }
    }
}
