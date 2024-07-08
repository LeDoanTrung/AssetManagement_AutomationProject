using AssetManagement.DataObjects;
using AssetManagement.Extenstions;
using AssetManagement.Library;
using OpenQA.Selenium;

namespace AssetManagement.Pages.AssetPage
{
    public class EditAssetPage : BasePage
    {
        //Web Element
        private Element _assetName = new Element(By.Id("assetName"));
        private Element _specification = new Element(By.Id("specification"));
        private Element _installedDate = new Element(By.Id("installedDate"));
        private Element _saveButton = new Element(By.XPath("//button[text()='Save']"));
        private Element _state(string state)
        {
            return new Element(By.XPath($"//label[text()='{state}']"));
        }

        //Method
        public void InputAssetName(string assetName)
        {
            _assetName.ClearTextFromTextArea();
            _assetName.InputText(assetName);
        }

        public void InputSpecification(string specification)
        {
            _specification.ClearTextFromTextArea();
            _specification.InputText(specification);
        }

        public void InputInstalledDate(string date)
        {
            string formattedDate = StringExtensions.ConvertDateFormat(date, "dd/MM/yyyy", "MM/dd/yyyy");
            _installedDate.SendKeys(formattedDate);
        }

        public void SelectState(string state)
        {
            _state(state).ClickWithScroll();
        }

        public void ClickOnSaveBtn()
        {
            _saveButton.IsElementEnabled();
            _saveButton.ClickWithScroll();
        }

        public void EditNewAsset(Asset asset)
        {
            Wait(3000); // Wait for loading data
            InputAssetName(asset.Name);
            InputSpecification(asset.Specification);
            InputInstalledDate(asset.InstalledDate);
            SelectState(asset.State);
            ClickOnSaveBtn();
        }
    }
}
