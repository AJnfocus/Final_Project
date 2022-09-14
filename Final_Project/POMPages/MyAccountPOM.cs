using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.POMPages {
    internal class MyAccountPOM {
        private IWebDriver driver;
        public MyAccountPOM(IWebDriver driver) { 
            this.driver = driver;
        }

        public IWebElement shopNavButton => driver.FindElement(By.LinkText("Shop"));

        public void shopButton() {
            shopNavButton.Click();
        }
    }
}
