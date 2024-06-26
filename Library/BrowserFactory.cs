using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;
using OpenQA.Selenium.Support.UI;

namespace AssetManagement.Library
{
    public static class BrowserFactory
    {
        [ThreadStatic]
        public static IWebDriver WebDriver;

        [ThreadStatic]
        public static WebDriverWait Wait;

        public static void InitDriver(string browserName)
        {
            switch (browserName.ToLower())
            {
                case "chrome":
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArguments("test-type");
                    chromeOptions.AddArguments("--no-sandbox");
                    WebDriver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), chromeOptions);
                    break;

                case "edge":
                    var edgeOptions = new EdgeOptions();
                    edgeOptions.AddArguments("headless");
                    edgeOptions.AddArguments("--no-sandbox");
                    WebDriver = new EdgeDriver(edgeOptions);
                    break;

                case "firefox":
                    var firefoxOptions = new FirefoxOptions();
                    firefoxOptions.AddArguments("headless");
                    WebDriver = new FirefoxDriver(firefoxOptions);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(browserName, "Browser not supported: " + browserName);
            }

            Wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(60));
        }

        public static void CloseDriver()
        {
            WebDriver.Quit();
        }
    }
}
