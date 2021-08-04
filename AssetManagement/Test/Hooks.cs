using NUnit.Framework;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using AssetManagement.Library;
using Microsoft.Extensions.Configuration;
using System.Threading;
using OpenQA.Selenium;

namespace AssetManagement.Test
{
    [SetUpFixture]
    public class Hooks
    {
        public static ExtentReports Extent;
        public static IConfiguration Config;
        public static ThreadLocal<IWebDriver> ThreadLocalWebDriver = new ThreadLocal<IWebDriver>();

        const string AppSettingPath = "Configurations\\appsettings.json";

        [OneTimeSetUp]
        public void MySetup()
        {
            TestContext.Progress.WriteLine("=========>Global OneTimeSetUp");

            //Read Configuration file
            Config = ConfigurationHelper.ReadConfiguration(AppSettingPath);

            //Init Extend report
            var dir = TestContext.CurrentContext.TestDirectory + "\\";
            var actualPath = dir.Substring(0, dir.LastIndexOf("bin"));
            var projectPath = new Uri(actualPath).LocalPath;
            var reportPath = projectPath + ConfigurationHelper.GetConfigurationByKey("TestResult.FilePath");

            var htmlReporter = new ExtentHtmlReporter(reportPath);
            Extent = new ExtentReports();
            Extent.AttachReporter(htmlReporter);
            Extent.AddSystemInfo("Application under Test", "Assest Management");
            Extent.AddSystemInfo("Version", "1.0");
            Extent.AddSystemInfo("Environment", "Test Environment");
        }

        [OneTimeTearDown]
        public void MyTeardown()
        {
            TestContext.Progress.WriteLine("=========>Global OneTimeTearDown");
            ThreadLocalWebDriver.Dispose();
            Extent.Flush();
        }
    }
}