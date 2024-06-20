using AssetManagement.Library;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Pages
{
    public class ManageUserPagecs : BasePage
    {
        //Web Element
        private Element createUserBtn = new Element(By.XPath("//button[text()=' Create New User']"));
        private Element searchBar = new Element(By.CssSelector("input[placeholder='Search']"));
        private Element searchIcon = new Element(By.CssSelector("input[placeholder='Search'] + button"));


    }
}
