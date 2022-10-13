using Final_Project.POMPages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Final_Project.Utility {
    [Binding]
    internal class Hook {

        public static IWebDriver driver;

        [BeforeScenario]
        public void setup() {
            string browser = Environment.GetEnvironmentVariable("BROWSER"); //Depending on the runsetting it will run the following browser 
            switch (browser) {
                case "firefox":
                    driver = new FirefoxDriver();
                    break;
                case "chrome":
                    driver = new ChromeDriver();
                    break;
                case "edge":
                    driver = new EdgeDriver();
                    break;
                default:
                    Console.WriteLine("No browser or unknown browser");
                    Console.WriteLine("Using Chrome instead");
                    driver = new ChromeDriver();
                    break;
            }
            driver.Manage().Window.Maximize(); //Makes the window full screen 
        }

        [AfterScenario]
        public void teardown() { 
            Thread.Sleep(2000);
            HelpersInstance help = new HelpersInstance(driver);
            NavBarPOM navBarPOM = new NavBarPOM(driver);
            MyAccountPOM myAccountPOM = new MyAccountPOM(driver);

            string count = navBarPOM.emptyCart.Text;
            if(count != "£0.00") {
                Console.WriteLine("Item still in basket");
                help.removeItems(driver);
                Thread.Sleep(2000);
                navBarPOM.accountButton();
                myAccountPOM.logout();
                help.WaitForElm(5, By.LinkText("Lost your password?"));
                driver.Quit();
            }
            else {
                Console.WriteLine("Logging Out");
                Thread.Sleep(2000);
                myAccountPOM.logout();
                help.WaitForElm(5, By.LinkText("Lost your password?"));
                driver.Quit();
            }
        }

    }
}
