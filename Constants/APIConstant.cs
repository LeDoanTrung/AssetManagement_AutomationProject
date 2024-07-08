using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Constants
{
    public class APIConstant
    {
        //Sign In Endpoint
        public const string SignInEndPoint = "/api/v1/auth/signIn";

        //Asset Endpoint
        public const string CreateNewAssetEndPoint = "/api/v1/assets";


        //Assignment Endpoint
        public const string CreateNewAssignmentEndPoint = "/api/v1/assignments";
        public const string ResponseAssignmentEndPoint = "/api/v1/assignments/response/{0}";
    }
}
