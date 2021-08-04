using System;
using NUnit.Framework;
using OpenQA.Selenium;
using AventStack.ExtentReports;
using NUnit.Framework.Interfaces;
using AssetManagement.Library;
using System.Threading;

namespace AssetManagement.Test
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    public abstract class BaseTest
    {
        public static ExtentTest Test;
        public static ExtentTest Node;

        [SetUp]
        public void Setup()
        {
            Hooks.ThreadLocalWebDriver.Value = BrowserFactory.InitDriver((ConfigurationHelper.GetConfigurationByKey("Browser")));
            GetWebDriver().Manage().Window.Maximize();
            Node = Test.CreateNode(TestContext.CurrentContext.Test.Name);
            Console.WriteLine("BaseTest Set up");
        }

        [TearDown]
        public void TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
            ? ""
            : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
            Status logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    var fileLocation = ScreenshotHelper.CaptureScreenshot(GetWebDriver(), TestContext.CurrentContext.Test.ClassName, TestContext.CurrentContext.Test.Name);
                    var mediaEntity = ScreenshotHelper.CaptureScreenShotAndAttachToExtendReport(GetWebDriver(), TestContext.CurrentContext.Test.Name);
                    Node.Fail("#Test Name: " + TestContext.CurrentContext.Test.Name + " #Status: " + logstatus + stacktrace, mediaEntity);
                    Node.Fail("#Screenshot Below: " + Node.AddScreenCaptureFromPath(fileLocation));
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    Node.Log(logstatus, "#Test Name: " + TestContext.CurrentContext.Test.Name + " #Status: " + logstatus);
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    Node.Skip("#Test Name: " + TestContext.CurrentContext.Test.Name + " #Status: " + logstatus);
                    break;
                default:
                    logstatus = Status.Pass;
                    Node.Log(logstatus, "#Test Name: " + TestContext.CurrentContext.Test.Name + " #Status: " + logstatus);
                    break;
            }
            GetWebDriver().Quit();
            GetWebDriver().Dispose();
            Console.WriteLine("BaseTest Tear Down");
        }

        [OneTimeSetUp]
        public void CreateTestForExtendReport()
        {
            Test = Hooks.Extent.CreateTest(TestContext.CurrentContext.Test.ClassName);
        }

        public static IWebDriver GetWebDriver()
        {
            return Hooks.ThreadLocalWebDriver.Value;
        }

    }
}