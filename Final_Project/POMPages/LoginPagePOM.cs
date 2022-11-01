using Final_Project.Utility;
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
        private IWebElement _loginField => driver.FindElement(By.Id("username")); //Finds the login   
        private IWebElement _passwordField => driver.FindElement(By.Id("password"));//Finds the password
        private IWebElement _submitButton => driver.FindElement(By.Name("login"));// Finds the sumbit button
        private IWebElement _errorEle => driver.FindElement(By.ClassName("woocommerce-error"));


        public LoginPagePOM setUsername(string username) {//Method to set the username 
            _loginField.Clear();
            _loginField.SendKeys(username);
            return this;
        }

        public LoginPagePOM setPassword(string password) { //Method to set the password 
            _passwordField.Clear();
            _passwordField.SendKeys(password);
            return this;
        }

        public void goSubmit() { //Method hit the sumbit
            _submitButton.Click();
        }

        //Helper method
        public Boolean LoginWithValidCredentials() { //Calls both of method above and have try and catch
            HelpersInstance wait = new HelpersInstance(driver);
            string username = Environment.GetEnvironmentVariable("username");
            string password = Environment.GetEnvironmentVariable("password"); //Gets the password from local file
            setUsername(username);
            setPassword(password);
            wait.takeScreenShot(driver, "Login"); 
            goSubmit();
            return true;
        }
    }
}
