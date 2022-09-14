using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.POMPages {
    internal class LoginPagePOM {
        private IWebDriver driver;
        public LoginPagePOM(IWebDriver driver) { 
            this.driver = driver;
        }
        public IWebElement loginField => driver.FindElement(By.Id("username"));
        public IWebElement passwordField => driver.FindElement(By.Id("password"));
        public IWebElement submitButton => driver.FindElement(By.Name("login"));

        public LoginPagePOM setUsername(string username) {
            loginField.Clear();
            loginField.SendKeys(username);
            return this;
        }

        public LoginPagePOM setPassword(string password) {
            passwordField.Clear();
            passwordField.SendKeys(password);
            //passwordField.SendKeys(password + Keys.Enter);
            return this;
        }

        public void goSubmit() {
            submitButton.Click();
        }

        //Helper method
        public Boolean LoginWithValidCredentials(string username, string password) {
            setUsername(username);
            setPassword(password);
            goSubmit();

            try {
                driver.SwitchTo().Alert(); //Attempt to switch to an alert. If we login there's no alert.
            }
            catch (Exception) {
                //No Alert so catch error
                //We must have logged in
                return true;
            }
            return false; // if there was an alert we didn't login
        }
    }
}
