using AssetManagement.DataObjects;
using AssetManagement.DataProvider;
using AssetManagement.Library.ReportHelper;
using AssetManagement.Pages;
using AssetManagement.Pages.AssetPage;
using AssetManagement.Pages.UserPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Test.AssetTest
{
    public class SearchAssetTest : BaseTest
    {
        private ManageAssetPage _manageAssetPage;
        private Asset createdAsset;

        [SetUp]
        public void BeforeSearchAssetTest()
        {
            createdAsset = AssetDataProvider.CreateRandomValidAsset();
        }

        [Test, Description("Search asset by name")]
        [TestCase("valid_admin")]
        public void SearchAssetByNameWithAssociatedResult(string accountKey)
        {
            Account valid_user = AccountData.GetAccount(accountKey);
             
            ExtentReportHelper.LogTestStep("Login");
            HomePage _homePage = _loginPage.Login(valid_user);

            ExtentReportHelper.LogTestStep("Go to Manage Asset page");
            _manageAssetPage = _homePage.NavigateToMangeAssetPage();

            ExtentReportHelper.LogTestStep("Create new asset for searching");
            CreateNewAssetPage _createNewAssetPage = _manageAssetPage.GoToCreateAssetPage();
            _createNewAssetPage.CreateNewAsset(createdAsset);

            ExtentReportHelper.LogTestStep("Search asset by name");
            _manageAssetPage.EnterSearchKeyword(createdAsset.Name);

            ExtentReportHelper.LogTestStep("Verify search result");
            _manageAssetPage.WaitForLoading();
            _manageAssetPage.VerifySearchAssetWithAssociatedResult(createdAsset.Name);

            _manageAssetPage.StoreDataToDelete();
        }

        [TearDown]
        public void AfterSearchAssetTest()
        {
            _manageAssetPage.DeleteCreatedAssetFromStorage();
        }
    }
}
