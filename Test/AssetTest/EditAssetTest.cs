﻿using AssetManagement.DataObjects;
using AssetManagement.DataProvider;
using AssetManagement.Library.ReportHelper;
using AssetManagement.Pages;
using AssetManagement.Pages.AssetPage;
using AssetManagement.Pages.UserPage;
using AssetManagement.Updater;


namespace AssetManagement.Test.AssetTest
{
    public class EditAssetTest : BaseTest
    {
        private ManageAssetPage _manageAssetPage;

        [Test, Description("Edit asset successfully")]
        [TestCase("valid_admin")]
        public void EditAssetWithValidDataSuccessfully(string accountKey)
        {
            Account valid_user = AccountData[accountKey];
            Asset beforeEditAsset = AssetDataProvider.CreateRandomValidAsset();
            Asset afterEditAsset = AssetDataProvider.CreateRandomValidAssetForEditting();

            ExtentReportHelper.LogTestStep("Login");
            HomePage _homePage = _loginPage.Login(valid_user);

            ExtentReportHelper.LogTestStep("Go to Manage Asset page");
            _manageAssetPage = _homePage.NavigateToMangeAssetPage();

            ExtentReportHelper.LogTestStep("Create new asset with valid data");
            CreateNewAssetPage _createNewAssetPage = _manageAssetPage.GoToCreateAssetPage();
            _createNewAssetPage.CreateNewAsset(beforeEditAsset);

            ExtentReportHelper.LogTestStep("Edit the created asset");
            EditAssetPage _editAssetPage = _manageAssetPage.GoToEditAsset();
            _editAssetPage.EditNewAsset(afterEditAsset);
            Asset expectedAsset = AssetUpdater.CreateExpectedAsset(beforeEditAsset, afterEditAsset);

            ExtentReportHelper.LogTestStep("Verify user information");
            _manageAssetPage.VerifyAssetInformation(expectedAsset);
            _manageAssetPage.CloseModal();
            _manageAssetPage.StoreDataToDelete();
        }

        [TearDown]
        public void AfterEditUserTest()
        {
            _manageAssetPage.DeleteCreatedAssetFromStorage();
        }
    
    }
}