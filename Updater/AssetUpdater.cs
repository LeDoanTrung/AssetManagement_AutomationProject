using AssetManagement.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Updater
{
    public static class AssetUpdater
    {
        public static Asset CreateExpectedAsset(Asset beforeEditAsset, Asset afterEditAsset)
        {
            return new Asset
            {
                Name = afterEditAsset.Name,
                Category = beforeEditAsset.Category, 
                Specification = afterEditAsset.Specification,
                InstalledDate = afterEditAsset.InstalledDate,
                State = afterEditAsset.State
            };
        }
    }
}
