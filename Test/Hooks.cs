using AssetManagement.Library;
using AssetManagement.Library.ShareData;
using Microsoft.Extensions.Configuration;


namespace AssetManagement.Test
{
    [SetUpFixture]
    public class Hooks
    {
        public static IConfiguration Config;

        const string AppSettingPath = "Configurations\\appsettings.json";

        [OneTimeSetUp]
        public void MySetup()
        {
            TestContext.Progress.WriteLine("====> Global one time setup");

            // Read Configuration file
            Config = ConfigurationHelper.ReadConfiguration(AppSettingPath);
            DataStorage.InitData();
        }
    }
}
