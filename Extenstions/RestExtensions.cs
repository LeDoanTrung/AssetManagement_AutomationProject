using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Extenstions
{
    public static class RestExtensions
    {
        public static dynamic ConvertToDynamicObject(this RestResponse response)
        {
            return (dynamic)JsonConvert.DeserializeObject(response.Content);
        }

        public static void VerifyStatusCodeOK(this RestResponse response)
        {
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        public static void VerifyStatusCodeCreated(this RestResponse response)
        {
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        public static void VerifyStatusCodeNoContent(this RestResponse response)
        {
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        public static void VerifyStatusCodeUnauthorized(this RestResponse response)
        {
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        public static void VerifyStatusCodeBadRequest(this RestResponse response)
        {
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
