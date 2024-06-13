using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NUnit.Framework.Interfaces;

namespace AssetManagement.Library.ReportHelper
{
    public class ExtentReportHelper
    {
        static ExtentReports ExtentManager;

        [ThreadStatic]
        public static ExtentTest ExtentTest;

        [ThreadStatic]
        public static ExtentTest Node;

        public static void InitializeReport(string reportPath, string hostName, string environment, string browser)
        {
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(reportPath);
            ExtentManager = new ExtentReports();
            ExtentManager.AttachReporter(htmlReporter);
            ExtentManager.AddSystemInfo("Host Name", hostName);
            ExtentManager.AddSystemInfo("Environment", environment);
            ExtentManager.AddSystemInfo("Browser", browser);
            Console.WriteLine("Initialize report");

        }

        public static void Flush()
        {
            Console.WriteLine("before flush");
            ExtentManager.Flush();
            Console.WriteLine("after flush");
        }

        public static void CreateTest(string name)
        {
            ExtentTest = ExtentManager.CreateTest(name);
            Console.WriteLine("create test");
        }

        public static void CreateNode(string name)
        {
            Node = ExtentTest.CreateNode(name);
            Console.WriteLine("create node");
        }

        public static void LogTestStep(string step)
        {
            Node.Info(step);
        }

        public static void CreateTestResult(TestStatus status, string stacktrace, string className, string testName, IWebDriver driver)
        {
            Status logstatus;
            try
            {
                switch (status)
                {
                    case TestStatus.Failed:
                        logstatus = Status.Fail;
                        var fileLocation = CaptureScreenshot(driver, className, testName);
                        testName = SanitizeFileName(testName);

                        Node.Fail("#Test Name: " + testName + " #Status: " + logstatus + stacktrace);
                        Node.Fail("#Screenshot Below: " + Node.AddScreenCaptureFromPath(fileLocation));
                        break;

                    case TestStatus.Inconclusive:
                        logstatus = Status.Warning;
                        Node.Log(logstatus, "#Test Name: " + testName + " #Status: " + logstatus);
                        break;

                    case TestStatus.Skipped:
                        logstatus = Status.Skip;
                        Node.Skip("#Test Name: " + testName + " #Status: " + logstatus);
                        break;

                    default:
                        logstatus = Status.Pass;
                        Node.Log(logstatus, "#Test Name: " + testName + " #Status: " + logstatus);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in CreateTestResult: " + ex.Message);
            }
        }

        public static string CaptureScreenshot(IWebDriver driver, string className, string testName)
        {
            try
            {
                ITakesScreenshot ts = (ITakesScreenshot)driver;
                Screenshot screenshot = ts.GetScreenshot();
                var screenshotDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Screenshot.Folder", className);
                testName = testName.Replace("\"", "");
                var fileName = string.Format(@"Screenshot_{0}_{1}", testName, DateTime.Now.ToString("yyyyMMdd_HHmmssff"));
                Directory.CreateDirectory(screenshotDirectory);
                var fileLocation = string.Format(@"{0}\{1}.png", screenshotDirectory, fileName);
                screenshot.SaveAsFile(fileLocation);
                return fileLocation;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in CaptureScreenshot: " + ex.Message);
                throw;
            }
        }

        public static MediaEntityModelProvider CaptureScreenShotAndAttachToExtendReport(IWebDriver driver, string screenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            var screenshot = ts.GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenShotName).Build();
        }

        private static string SanitizeFileName(string fileName)
        {
            fileName = Regex.Replace(fileName, @"[^a-zA-Z0-9_\-]", "_");

            const int maxLength = 80;
            if (fileName.Length > maxLength)
            {
                fileName = fileName.Substring(0, maxLength);
            }

            return fileName;
        }
    }
}
