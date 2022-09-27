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
            this.driver = driver; // Constructor 
        }
        public IWebElement loginField => driver.FindElement(By.Id("username")); //Finds the login   
        public IWebElement passwordField => driver.FindElement(By.Id("password"));//Finds the password
        public IWebElement submitButton => driver.FindElement(By.Name("login"));// Finds the sumbit button

        public LoginPagePOM setUsername(string username) {//Method to set the username 
            loginField.Clear();
            loginField.SendKeys(username);
            return this;
        }

        public LoginPagePOM setPassword(string password) { //Method to set the password 
            passwordField.Clear();
            passwordField.SendKeys(password);
            return this;
        }

        public void goSubmit() { //Method hit the sumbit
            submitButton.Click();
        }

        //Helper method
        public Boolean LoginWithValidCredentials() { //Calls both of method above and have try and catch
            string username = Environment.GetEnvironmentVariable("username");
            string password = Environment.GetEnvironmentVariable("password");

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
