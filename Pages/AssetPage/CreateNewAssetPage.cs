using AssetManagement.DataObjects;
using AssetManagement.Library;
using AssetManagement.Library.Util;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Pages.AssetPage
{
    public class CreateNewAssetPage : BasePage
    {
        //Web Element
        private Element _assetName = new Element(By.Id("assetName"));
        private Element _categoryDropdown = new Element(By.Name("categoryName"));
        private Element _specification = new Element(By.Id("specification"));
        private Element _installedDate = new Element(By.Id("installedDate"));
        private Element _saveButton = new Element(By.XPath("//button[text()='Save']"));
        private Element _state(string state)
        {
            return new Element(By.XPath($"//label[text()='{state}']"));
        }

        private Element _categoryValue(string category)
        {
            return new Element(By.XPath($"//a[text()='{category}']"));
        }

        //Method
        public void InputAssetName(string assetName)
        {
            _assetName.ClearText();
            _assetName.InputText(assetName);
        }

        public void SelectCategory(string category)
        {
            _categoryDropdown.ClickWithScroll();
            _categoryValue(category).ClickWithScroll();
        }

        public void InputSpecification(string specification)
        {
            _specification.ClearText();
            _specification.InputText(specification);
        }

        public void InputInstalledDate(string date)
        {
            string formattedDate = StringUtility.ConvertDateFormat(date, "dd/MM/yyyy", "MM/dd/yyyy");
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
        public void CreateNewAsset(Asset asset)
        {
            InputAssetName(asset.Name);
            SelectCategory(asset.Category);
            InputSpecification(asset.Specification);
            InputInstalledDate(asset.InstalledDate);
            SelectState(asset.State);
            ClickOnSaveBtn();
        }


    }
}
