using AssetManagement.Constants;
using AssetManagement.DataObjects;
using AssetManagement.DataProvider;
using AssetManagement.Library;
using AssetManagement.Library.API;
using AssetManagement.Library.ReportHelper;
using AssetManagement.Library.Util;
using AssetManagement.Library.Utils;
using AssetManagement.Pages.LoginPage;


namespace AssetManagement.Test
{
    [TestFixture]
    public abstract class BaseTest
    {
        protected UserDataProvider AccountData;
        protected string loginUrl = ConfigurationHelper.GetConfigurationByKey(Hooks.Config, "TestURL");
        protected LoginPage _loginPage;
        public BaseTest()
        {
            AccountData = new UserDataProvider(FileConstant.AccountFilePath.GetAbsolutePath());
            _loginPage = new LoginPage();
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            string environment = ConfigurationHelper.GetConfigurationByKey(Hooks.Config, "Environment");
            string browser = ConfigurationHelper.GetConfigurationByKey(Hooks.Config, "Browser");
            string reportPath = CreateFolderStructure();
            ExtentReportHelper.InitializeReport(reportPath, "Asset Management", environment, browser);
        }

        [SetUp]
        public void Setup()
        {
            string browser = ConfigurationHelper.GetConfigurationByKey(Hooks.Config, "Browser");
            double timeOutSec = double.Parse(ConfigurationHelper.GetConfigurationByKey(Hooks.Config, "timeout.webdriver.wait.seconds"));
            string pageLoadTime = ConfigurationHelper.GetConfigurationByKey(Hooks.Config, "timeout.webdriver.pageLoad.seconds");
            string asyncJsTime = ConfigurationHelper.GetConfigurationByKey(Hooks.Config, "timeout.webdriver.asyncJavaScript.seconds");


            InitializeWebDriver(browser, timeOutSec, pageLoadTime, asyncJsTime);

            ExtentReportHelper.CreateTest(TestContext.CurrentContext.Test.ClassName);
            ExtentReportHelper.CreateNode(TestContext.CurrentContext.Test.Name);

            GoToLoginPage();

            Console.WriteLine("Base Test Set up");
        }

        private string CreateFolderStructure()
        {
            //Create folder "TestResults"
            string projectDirectory = Directory.GetCurrentDirectory();
            string testResultsDirectory = Path.Combine(projectDirectory, "TestResults");
            Directory.CreateDirectory(testResultsDirectory);

            // Return the path to the index.html file
            string reportPath = Path.Combine(testResultsDirectory, "index.html");
            return reportPath;
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
            //BrowserFactory.WebDriver.Manage().Window.Maximize();
            BrowserFactory.WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeOutSec);
            BrowserFactory.WebDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(double.Parse(pageLoadTime));
            BrowserFactory.WebDriver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(double.Parse(asyncJsTime));
        }

        public void GoToLoginPage()
        {
            ExtentReportHelper.LogTestStep("Go to Login page.");
            BrowserFactory.WebDriver.Url = loginUrl;
        }

        [TearDown]
        public void TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace) ? "" : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);

            ExtentReportHelper.CreateTestResult(status, stacktrace, TestContext.CurrentContext.Test.ClassName, TestContext.CurrentContext.Test.Name, BrowserFactory.WebDriver);

            BrowserFactory.WebDriver.Quit();

            Console.WriteLine("Base Test Tear Down");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            ExtentReportHelper.Flush();
        }

    }
}
