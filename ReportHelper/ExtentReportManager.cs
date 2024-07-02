using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System.Reflection;

namespace DemoQA.Core.ExtentReport
{
    public class ExtentReportManager
    {
        private static readonly Lazy<ExtentReports> _lazyReport = new Lazy<ExtentReports> (() => new ExtentReports ());
        public static ExtentReports Instance { get {  return _lazyReport.Value; } }

        static ExtentReportManager()
        {
            // Get report path
            string projectPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string reportPath = Path.Combine(projectPath, "TestResults");

            if (!Directory.Exists(reportPath))
            {
                Directory.CreateDirectory(reportPath);
            }

            // Config html Reporter
            var htmlReporter = CreateHtmlReporter(reportPath);
            Instance.AttachReporter(htmlReporter);
        }

        private static ExtentHtmlReporter CreateHtmlReporter(string reportPath)
        {
            var htmlReporter = new ExtentHtmlReporter(Path.Combine(reportPath, "index.html"));

            // Setting the configuration directly in code
            htmlReporter.Config.ReportName = "Test Automation Report";
            htmlReporter.Config.Encoding = "UTF-8";
            htmlReporter.Config.DocumentTitle = "Test Automation Report";
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;

            return htmlReporter;
        }


        public static void GenerateReport()
        {
            Instance.Flush();
        }
    }
}
