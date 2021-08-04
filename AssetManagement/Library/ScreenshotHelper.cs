using System;
using System.IO;
using AventStack.ExtentReports;
using AssetManagement.Test;
using OpenQA.Selenium;

namespace AssetManagement.Library
{
    public static class ScreenshotHelper
    {
        public static string CaptureScreenshot(IWebDriver driver, string className, string testName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            var screenshotDirectory = Path.Combine(Directory.GetCurrentDirectory(), ConfigurationHelper.GetConfigurationByKey("Screenshot.Folder"), className);
            testName = testName.Replace("\"", "");
            var fileName = string.Format(@"Screenshot_{0}_{1}", testName, DateTime.Now.ToString("yyyyMMdd_HHmmssff"));
            Directory.CreateDirectory(screenshotDirectory);
            var fileLocation = string.Format(@"{0}\{1}.png", screenshotDirectory, fileName);
            screenshot.SaveAsFile(fileLocation, ScreenshotImageFormat.Png);
            return fileLocation;
        }

        public static MediaEntityModelProvider CaptureScreenShotAndAttachToExtendReport(IWebDriver driver, String screenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            var screenshot = ts.GetScreenshot().AsBase64EncodedString;

            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenShotName).Build();
        }
    }
}