using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.POMPages {
    internal class HomePagePOM {
        private IWebDriver driver;
        public HomePagePOM(IWebDriver driver) {
            this.driver = driver;
        }

        public IWebElement loginField => driver.FindElement(By.Id("#username"));
        public IWebElement passwordField => driver.FindElement(By.Id("#password"));
        public IWebElement submitButton => driver.FindElement(By.LinkText("Log in"));

    }
}