using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace AssetManagement.Library
{
    public static class BrowserFactory
    {
        public static IWebDriver InitDriver(string browserName)
        {
            switch (browserName.ToLower())
            {
                case "chrome":
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArguments("test-type");
                    chromeOptions.AddArguments("headless");
                    chromeOptions.AddArguments("--window-size=1325x744");
                    chromeOptions.AddArgument("--no-sandbox");
                    return new ChromeDriver(chromeOptions);
                case "edge":
                    new DriverManager().SetUpDriver(new EdgeConfig());
                    var edgeOptions = new EdgeOptions();
                    edgeOptions.AddArguments("headless");
                    edgeOptions.AddArguments("--window-size=1325x744");
                    edgeOptions.AddArgument("--no-sandbox");
                    return new EdgeDriver(edgeOptions);
                case "firefox":
                    new DriverManager().SetUpDriver(new FirefoxConfig());
                    var firefoxOptions = new FirefoxOptions();
                    firefoxOptions.AddArguments("headless");
                    firefoxOptions.AddArguments("--window-size=1325x744");
                    return new FirefoxDriver();
                default:
                    throw new ArgumentOutOfRangeException(browserName);
            }
        }
    }
}