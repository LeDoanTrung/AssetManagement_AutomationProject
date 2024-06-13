using AssetManagement.Library;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
