using AssetManagement.Constants;
using AssetManagement.DataObjects;
using AssetManagement.Extenstions;
using AssetManagement.Library;
using AssetManagement.Library.ReportHelper;
using AssetManagement.Library.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Test
{
    [TestFixture]
    public abstract class BaseTest
    {
        protected Dictionary<string, Account> AccountData;

        public BaseTest()
        {
            AccountData = JsonHelper.ReadAndParse<Dictionary<string, Account>>(FileConstant.AccountFilePath.GetAbsolutePath());
        }

        [SetUp]
        public void Setup()
        {
            string enviroment = ConfigurationHelper.GetConfigurationByKey(Hooks.Config, "Enviroment");
            string browser = ConfigurationHelper.GetConfigurationByKey(Hooks.Config, "Browser");
            double timeOutSec = double.Parse(ConfigurationHelper.GetConfigurationByKey(Hooks.Config, "timeout.webdriver.wait.seconds"));
            string pageLoadTime = ConfigurationHelper.GetConfigurationByKey(Hooks.Config, "timeout.webdriver.pageLoad.seconds");
            string asyncJsTime = ConfigurationHelper.GetConfigurationByKey(Hooks.Config, "timeout.webdriver.asyncJavaScript.seconds");
            string date = DateTime.Now.ToString("ddMMMyyyy_HHmmss");
            string functionName = TestContext.CurrentContext.Test.MethodName;

            //Create folder structure
            string functionTestDirectory = CreateFolderStructure(date, functionName);

            //Initialize report
            string reportPath = Path.Combine(functionTestDirectory, "result.html");
            InitializeReport(reportPath, "DemoQA", enviroment, browser);

            //Initialize WebDriver
            InitializeWebDriver(browser, timeOutSec, pageLoadTime, asyncJsTime);

            Console.WriteLine("Base Test Set up");
        }

        private string CreateFolderStructure(string date, string functionName)
        {
            //Create folder "TestResults"
            string projectDirectory = Directory.GetCurrentDirectory();
            string testResultsDirectory = Path.Combine(projectDirectory, "TestResults");
            Directory.CreateDirectory(testResultsDirectory);

            //Create subfolder by Test Class's name
            string testClassName = TestContext.CurrentContext.Test.ClassName;
            string classNameWithoutNamespace = testClassName.Substring(testClassName.LastIndexOf('.') + 1);

            string classTestDirectory = Path.Combine(testResultsDirectory, classNameWithoutNamespace);
            Directory.CreateDirectory(classTestDirectory);

            //Create subfolder by Test Case's name
            string functionTestDirectory = Path.Combine(classTestDirectory, $"Result_{date}_{functionName}");
            Directory.CreateDirectory(functionTestDirectory);

            return functionTestDirectory;
        }

        private void InitializeReport(string reportPath, string systemName, string environment, string browser)
        {
            ExtentReportHelper.InitializeReport(reportPath, systemName, environment, browser);
            ExtentReportHelper.CreateTest(TestContext.CurrentContext.Test.ClassName);
            ExtentReportHelper.CreateNode(TestContext.CurrentContext.Test.Name);
            ExtentReportHelper.LogTestStep("Initialize webdriver");
        }

        private void InitializeWebDriver(string browser, double timeOutSec, string pageLoadTime, string asyncJsTime)
        {
            BrowserFactory.InitDriver(browser);
            BrowserFactory.WebDriver.Manage().Window.Maximize();
            BrowserFactory.WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeOutSec);
            BrowserFactory.WebDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(double.Parse(pageLoadTime));
            BrowserFactory.WebDriver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(double.Parse(asyncJsTime));
        }

        [TearDown]
        public void TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace) ? "" : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);

            ExtentReportHelper.CreateTestResult(status, stacktrace, TestContext.CurrentContext.Test.ClassName, TestContext.CurrentContext.Test.Name, BrowserFactory.WebDriver);
            ExtentReportHelper.Flush();

            BrowserFactory.WebDriver.Quit();

            Console.WriteLine("Base Test Tear Down");
        }

    }
}
