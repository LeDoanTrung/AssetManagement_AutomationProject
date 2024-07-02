using AventStack.ExtentReports;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System.Threading.Tasks;


namespace DemoQA.Core.ExtentReport
{
    public class ExtentTestManager
    {
        private static AsyncLocal<ExtentTest> _parentTest = new AsyncLocal<ExtentTest>();
        private static AsyncLocal<ExtentTest> _childTest = new AsyncLocal<ExtentTest>();

        public static ExtentTest CreateParentTest(string testName, string description = null)
        {
            _parentTest.Value = ExtentReportManager.Instance.CreateTest(testName, description);
            return _parentTest.Value;
        }

        public static ExtentTest CreateTest(string testName, string description = null)
        {
            _childTest.Value = _parentTest.Value.CreateNode(testName, description);
            return _childTest.Value;
        }

        public static ExtentTest GetTest()
        {
            return _childTest.Value;
        }

        public static void UpdateTestReport()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace) ? "" : TestContext.CurrentContext.Result.StackTrace;
            var message = TestContext.CurrentContext.Result.Message;

            switch (status)
            {
                case TestStatus.Failed:
                    ReportLog.Fail($"Test failed with message: {message}");
                    ReportLog.Fail($"Stacktrace: {stacktrace}");
                    break;
                case TestStatus.Inconclusive:
                    ReportLog.Skip($"Test inconclusive with message: {message}");
                    ReportLog.Skip($"Stacktrace: {stacktrace}");
                    break;
                case TestStatus.Skipped:
                    ReportLog.Skip($"Test skipped with message: {message}");
                    break;
                default:
                    ReportLog.Pass("Test passed");
                    break;
            }
        }
    }
}
