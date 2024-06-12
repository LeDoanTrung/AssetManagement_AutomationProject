using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using DemoNunitFW.Report;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DemoNunitFW
{
    public class TestExtentReport
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            ExtentReportHelper.InitializeReport("Result\\result.html", "Hostname", "Staging", "Chrome");
            ExtentReportHelper.CreateTest(TestContext.CurrentContext.Test.ClassName);
            ExtentReportHelper.CreateNode(TestContext.CurrentContext.Test.Name);
            ExtentReportHelper.LogTestStep("Initialize webdriver");  
            this.driver = new ChromeDriver(ChromeDriverService.CreateDefaultService()); 
        }

        [Test]
        public void Test1()
        {
            ExtentReportHelper.LogTestStep("Go to google page");
            driver.Url = "https://google.com";  
            //Assert.True(false);    
            ExtentReportHelper.LogTestStep("After assert");
        }

        [TearDown]
        public void TearDown(){
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
            ? ""
            : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
            ExtentReportHelper.CreateTestResult(status, stacktrace, TestContext.CurrentContext.Test.ClassName, TestContext.CurrentContext.Test.Name, this.driver);
            ExtentReportHelper.Flush();
            this.driver.Quit();
            this.driver.Dispose();
        }
    }
}