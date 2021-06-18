using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PowerService.Util
{
    public class UserProfileFunctions
    {
        internal class ManagementAccessToken
        {
            internal string AccessToken { get; set; }
            internal string TokenType { get; set; }
        }
        internal string GetAuth0ManagementAPIToken(string domain, string clientid, string client_secret, string audience)
        {
            var uri = $"https://{domain}/oauth/token";
            var client = new RestClient(uri);

            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\"client_id\":\"" + clientid + "\",\"client_secret\":\"" + client_secret + "\",\"audience\":\"" + audience+ "\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
           
            var tokenInfo = JsonConvert.DeserializeObject<ManagementAccessToken>(response.Content);
            return tokenInfo.AccessToken;
        }
        internal Data.Models.PortalUser GetUserIdentity(string domain, string userId)
        {
            var uri = $"https://{domain}/v2/users/{userId}";
            var client = new RestClient(uri);
            var request = new RestRequest(Method.GET);
            request.AddHeader("authorization", "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6Ikxocmd1N3pwTVk2YVNRZ0NSanZSQyJ9.eyJpc3MiOiJodHRwczovL2ZpZ21lbnQuZXUuYXV0aDAuY29tLyIsInN1YiI6Ik0xc05KY2d4QTJROWdCUHNQOVJrMlM2NzJLcXc2S3NwQGNsaWVudHMiLCJhdWQiOiJodHRwczovL2ZpZ21lbnQuZXUuYXV0aDAuY29tL2FwaS92Mi8iLCJpYXQiOjE2MjM5MjQxODcsImV4cCI6MTYyNDAxMDU4NywiYXpwIjoiTTFzTkpjZ3hBMlE5Z0JQc1A5UmsyUzY3MktxdzZLc3AiLCJndHkiOiJjbGllbnQtY3JlZGVudGlhbHMifQ.iXl17sJvBtj7khm_Rx9L-D2XExK5RKk2_4s_8ls1oVphqewbCw2bE9-nDnQuKAafEyopuo1vr7PenNJjGy5IA1d22TzJtIDCjBwtZWY9FZ7bbBXHuI17ENCwWi0JA07Os1nICYpDPAkQVVOs8_Ge9KXXBgr8WuIEkF6GAVw214XBdB4GomdrzSKg3Ryw46kRjknkSSAlidN_PjW_ktvhnyOAC0MA3W0Nn_Go-suVz3WdhhcDm8e0utCJp9jIe92Nl9BYTROqUfZV2B_uUh0SytnfAxz0YMDdSB-AVar8Avw3J2apa3D3nBU_G2u3fVrRYAWqDuNNa8Lzhl8Y1tz_hQ");
            IRestResponse response = client.Execute(request);
            // var user = UserManager.FindByName(User.Identity.Name);
            return new Data.Models.PortalUser();//user.Email
        }
    }
}
