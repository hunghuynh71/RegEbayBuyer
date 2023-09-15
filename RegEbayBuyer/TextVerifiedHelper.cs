using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreDogeTool
{
    public static class TextVerifiedHelper
    {
        public static string GetBearToken(string simpleApiAccessToken)
        {
            try
            {
                var client = new RestClient("https://www.textverified.com/Api/SimpleAuthentication");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("X-SIMPLE-API-ACCESS-TOKEN", simpleApiAccessToken);
                IRestResponse response = client.Execute(request);

                if (!response.StatusCode.ToString().Equals("OK"))
                    return "";

                var jsonResult = (JObject)JsonConvert.DeserializeObject(response.Content);
                return jsonResult["bearer_token"].ToString();
            }
            catch { }

            return "";
        }

        public static string GetServiceID(string Authorization, string serviceName = "eBay")
        {
            try
            {
                var client = new RestClient("https://www.textverified.com/api/targets");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", $"Bearer {Authorization}");
                IRestResponse response = client.Execute(request);

                var jsonResult = (JArray)JsonConvert.DeserializeObject(response.Content);
                
                for (int i = 0; i < jsonResult.Count; i++)
                {
                    try
                    {
                        if (jsonResult[i]["name"].ToString().Equals(serviceName))
                            return jsonResult[i]["targetId"].ToString();
                    }
                    catch {  }
                }
            }
            catch { }

            return "";
        }


        public static void CancelVerification(string bearer, string id)
        {
            try
            {
                var client = new RestClient($"https://www.textverified.com/api/Verifications/{id}/Cancel");
                client.Timeout = -1;
                var request = new RestRequest(Method.PUT);
                request.AddHeader("Authorization", $"Bearer {bearer}");
                //request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjdkMjJiYWIwLWZmMTktNDM2Ny05YzFkLTMzODY2OWEyOGQ1MCIsImV4cCI6MTYyNzUxODI2NCwiaXNzIjoidGV4dHZlcmlmaWVkLmNvbSIsImF1ZCI6InRleHR2ZXJpZmllZC5jb20ifQ.Ngyk15x4yyJkEKDK1tSWu9U-tisc0PtXnbKIIdN321s");
                request.AddHeader("Content-Type", "application/json");
                IRestResponse response = client.Execute(request);
            }
            catch { }
        }



        public static string CreateVerification(string bearer, string serviceID)
        {
            try
            {
                var client = new RestClient("https://www.textverified.com/api/Verifications");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", $"Bearer {bearer}");
                //request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjdkMjJiYWIwLWZmMTktNDM2Ny05YzFkLTMzODY2OWEyOGQ1MCIsImV4cCI6MTYyNzUxODI2NCwiaXNzIjoidGV4dHZlcmlmaWVkLmNvbSIsImF1ZCI6InRleHR2ZXJpZmllZC5jb20ifQ.Ngyk15x4yyJkEKDK1tSWu9U-tisc0PtXnbKIIdN321s");
                request.AddHeader("Content-Type", "application/json");
                var body = "{\"id\":" + serviceID + "}";
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                if (!response.StatusCode.ToString().Equals("OK"))
                    return "|";

                var jsonResult = (JObject)JsonConvert.DeserializeObject(response.Content);
                return jsonResult["id"].ToString() + "|" + jsonResult["number"].ToString();

            }
            catch { }

            return "|";
        }

        public static string GetVerificationDetails(string Authorization, string id, int timeout)
        {
            for (int i = 0; i < timeout /2; i++)
            {
                string code = GetVerificationDetails(Authorization, id);
                if (!code.Equals(""))
                    return code;

                Thread.Sleep(2000);
            }

            return "";
        }

        public static string GetVerificationDetails(string Authorization, string id)
        {
            try
            {
                var client = new RestClient($"https://www.textverified.com/api/Verifications/{id}");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", $"Bearer {Authorization}");
                //request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjdkMjJiYWIwLWZmMTktNDM2Ny05YzFkLTMzODY2OWEyOGQ1MCIsImV4cCI6MTYyNzUxODI2NCwiaXNzIjoidGV4dHZlcmlmaWVkLmNvbSIsImF1ZCI6InRleHR2ZXJpZmllZC5jb20ifQ.Ngyk15x4yyJkEKDK1tSWu9U-tisc0PtXnbKIIdN321s");
                IRestResponse response = client.Execute(request);
                
                if (!response.StatusCode.ToString().Equals("OK"))
                    return "";

                var jsonResult = (JObject)JsonConvert.DeserializeObject(response.Content);
                if (jsonResult["status"].ToString().Equals("Completed"))
                {
                    return jsonResult["code"].ToString();
                }

            }
            catch { }

            return "";
        }
    }
}
