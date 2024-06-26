using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Pages
{
    public class HomePage : BasePage
    {
        
        //Method
        public void IsAtHomePage()
        {
            userName_value.IsElementDisplayed().Should().BeTrue();
        }


    }
}
