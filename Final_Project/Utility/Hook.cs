using AventStack.ExtentReports;
using AventStack.ExtentReports.Core;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Gherkin.Model;
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
using System.Security.Policy;
using System.Reflection;

namespace Final_Project.Utility {
    [Binding]
    internal class Hook {

        public static IWebDriver driver;
        private static AventStack.ExtentReports.ExtentReports extent;
        private static ExtentTest featureName;
        private static ExtentTest scenario;

        [BeforeTestRun]
        public static void BeforeTestRun() {
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(@"C:\Users\AaronJavier\source\repos\Final_Project\Final_Project\Result\test1.html");
            extent = new AventStack.ExtentReports.ExtentReports();
            extent.AttachReporter(htmlReporter);
        }

        [AfterTestRun]
        public static void AfterTestRun() {
            extent.Flush();
        }

        [BeforeFeature]
        public static void BeforeFeature() {
            featureName = extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);
        }

        [AfterStep]
        public void reportStep() {
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            PropertyInfo info = typeof(ScenarioContext).GetProperty("TestStatus", BindingFlags.Instance | BindingFlags.NonPublic);
            if(ScenarioContext.Current.TestError == null) {
                if(stepType == "Given") {
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
                } else if (stepType == "When") {
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
                } else if(stepType == "Then") {
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
                } else if(stepType == "And") {
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text);
                }
            } else if(ScenarioContext.Current.TestError != null) {
                if(stepType == "Given") {
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
                } else if (stepType == "When") {
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
                } else if(stepType == "Then") {
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
                } else if (stepType == "And") {
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text);
                }
            }
        }

        [BeforeScenario]
        public void setup() {
            scenario = featureName.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);
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
