using AssetManagement.Constants;
using AssetManagement.DataObjects;
using AssetManagement.DataProvider;
using AssetManagement.Library.ReportHelper;
using AssetManagement.Pages;
using AssetManagement.Pages.AssetPage;
using AssetManagement.Pages.UserPage;



namespace AssetManagement.Test.AssetTest
{
    public class CreateAssetTest : BaseTest
    {
        private ManageAssetPage _manageAssetPage;

        [Test, Description("Create asset successfully")]
        [TestCase("valid_admin")]
        public void CreateNewAssetSuccessfully(string accountKey)
        {
            Account valid_user = AccountData.GetAccount(accountKey);
            Asset createdAsset = AssetDataProvider.CreateRandomValidAsset();

            ExtentReportHelper.LogTestStep("Login");
            HomePage _homePage = _loginPage.Login(valid_user);

            ExtentReportHelper.LogTestStep("Go to Manage Asset page");
            _manageAssetPage = _homePage.NavigateToMangeAssetPage();

            ExtentReportHelper.LogTestStep("Go to Create Asset page");
            CreateNewAssetPage _createNewAssetPage = _manageAssetPage.GoToCreateAssetPage();

            ExtentReportHelper.LogTestStep("Create new Asset with valid data");
            _createNewAssetPage.CreateNewAsset(createdAsset);

            ExtentReportHelper.LogTestStep("Verify asset information");
            _manageAssetPage.VerifyMessage(MessageConstant.CreateAsssetSuccessfullyMessage);
            _manageAssetPage.VerifyAssetInformation(createdAsset);
            _manageAssetPage.CloseModal();
            _manageAssetPage.StoreDataToDelete();
        }

        [TearDown]
        public void AfterCreateAssetTest()
        { 
            _manageAssetPage.DeleteCreatedAssetFromStorage();
        }
    }
}
