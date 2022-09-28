using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Utility {
    internal class TestBaseClass {

        protected IWebDriver driver;
        protected string baseURL = "https://www.edgewordstraining.co.uk/demo-site/";

        [SetUp]
        public void Setup() {
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

        [TearDown]
        public void TearDown() {
            Thread.Sleep(3000);  //Tears down the browser if it fails or passes 
            driver.Quit();
        }
    }
}
