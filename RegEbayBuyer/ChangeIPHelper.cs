using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreDogeTool
{
    class ChangeIPHelper
    {
        public static void ChangeIPHMA()
        {
            try
            {
                // [1] Turn Off HMA
                System.Diagnostics.Process pProcess_turnOff = new System.Diagnostics.Process();
                pProcess_turnOff.StartInfo.FileName = "C:\\Windows\\System32\\netsh";
                pProcess_turnOff.StartInfo.Arguments = "interface set interface \"HMA! Pro VPN\" disable";
                pProcess_turnOff.StartInfo.UseShellExecute = false;
                pProcess_turnOff.StartInfo.CreateNoWindow = true;
                pProcess_turnOff.StartInfo.RedirectStandardOutput = true;
                pProcess_turnOff.Start();

                string strOutput_turn_on = pProcess_turnOff.StandardOutput.ReadToEnd();
                pProcess_turnOff.WaitForExit();
                Thread.Sleep(TimeSpan.FromSeconds(18));

                // [2] Turn On HMA
                System.Diagnostics.Process pProcess_turnOn = new System.Diagnostics.Process();
                pProcess_turnOn.StartInfo.FileName = "C:\\Windows\\System32\\netsh";
                pProcess_turnOn.StartInfo.Arguments = "interface set interface \"HMA! Pro VPN\" enable";
                pProcess_turnOn.StartInfo.UseShellExecute = false;
                pProcess_turnOn.StartInfo.CreateNoWindow = true;
                pProcess_turnOn.StartInfo.RedirectStandardOutput = true;
                pProcess_turnOn.Start();

                string strOutput_turnOn = pProcess_turnOn.StandardOutput.ReadToEnd();
                pProcess_turnOn.WaitForExit();
                Thread.Sleep(TimeSpan.FromSeconds(20));
            }
            catch (Exception ex)
            {
                FileHelper.WriteAppendToFile(Directory.GetCurrentDirectory() + "\\log.txt", "error get ip 911 " + ex.Message);
            }

        }


        public static string GetIPByZipCode911(string zipcode, string port)
        {
            try
            {
                string path_911 = FileHelper.ReadFile(Directory.GetCurrentDirectory() + "\\path_911.txt").Trim();

                System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
                pProcess.StartInfo.FileName = path_911 + "\\ProxyAPI.exe";
                pProcess.StartInfo.Arguments = $"-changeproxy/US -zip={zipcode} -proxyport={port} -isp=\"{ConfigInfo.nhamang}\"";
                pProcess.StartInfo.UseShellExecute = false;
                pProcess.StartInfo.CreateNoWindow = true;
                pProcess.StartInfo.RedirectStandardOutput = true;
                pProcess.Start();

                string strOutput = pProcess.StandardOutput.ReadToEnd();
                pProcess.WaitForExit();
                return strOutput;
            }
            catch (Exception ex)
            {
                FileHelper.WriteAppendToFile(Directory.GetCurrentDirectory() + "\\log.txt", "error get ip 911 " + ex.Message);
            }

            return "";
        }

        public static string GetIPByCity911(string state, string city, string port)
        {
            try
            {
                string path_911 = FileHelper.ReadFile(Directory.GetCurrentDirectory() + "\\path_911.txt").Trim();

                System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
                pProcess.StartInfo.FileName = path_911 + "\\ProxyAPI.exe";
                pProcess.StartInfo.Arguments = $"-changeproxy/US/{state}/\"{city}\" -proxyport={port} -isp=\"{ConfigInfo.nhamang}\"";
                pProcess.StartInfo.UseShellExecute = false;
                pProcess.StartInfo.CreateNoWindow = true;
                pProcess.StartInfo.RedirectStandardOutput = true;
                pProcess.Start();

                string strOutput = pProcess.StandardOutput.ReadToEnd();
                pProcess.WaitForExit();
                return strOutput;
            }
            catch (Exception ex) 
            {
                FileHelper.WriteAppendToFile(Directory.GetCurrentDirectory() + "\\log.txt", "error get ip 911 " + ex.Message);
            }

            return "";
        }

        public static string GetIPByState911(string state, string port)
        {
            try
            {
                string path_911 = FileHelper.ReadFile(Directory.GetCurrentDirectory() + "\\path_911.txt").Trim();

                System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
                pProcess.StartInfo.FileName = path_911 + "\\ProxyAPI.exe";
                pProcess.StartInfo.Arguments = $"-changeproxy/US/{state} -proxyport={port}";
                pProcess.StartInfo.UseShellExecute = false;
                pProcess.StartInfo.CreateNoWindow = true;
                pProcess.StartInfo.RedirectStandardOutput = true;
                pProcess.Start();

                string strOutput = pProcess.StandardOutput.ReadToEnd();
                pProcess.WaitForExit();
                return strOutput;
            }
            catch (Exception ex)
            {
                FileHelper.WriteAppendToFile(Directory.GetCurrentDirectory() + "\\log.txt", "error get ip 911 " + ex.Message);
            }

            return "";
        }

        public static string GetIPByCountryPL911(string port, string path_911)
        {
            try 
            {
                System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
                pProcess.StartInfo.FileName = path_911 + "\\ProxyAPI.exe";
                pProcess.StartInfo.Arguments = $"-changeproxy/PL -proxyport={port}";
                pProcess.StartInfo.UseShellExecute = false;
                pProcess.StartInfo.CreateNoWindow = true;
                pProcess.StartInfo.RedirectStandardOutput = true;
                pProcess.Start();

                string strOutput = pProcess.StandardOutput.ReadToEnd();
                pProcess.WaitForExit();
                return strOutput;
            }
            catch (Exception ex)
            {
                FileHelper.WriteAppendToFile(Directory.GetCurrentDirectory() + "\\log.txt", "error get ip 911 " + ex.Message);
            }

            return "";
        }

        public static string GetProxyTinsoft(string tinsoftKey)
        {
            try
            {
                var client = new RestClient("http://proxy.tinsoftsv.com/api/changeProxy.php?key=" + tinsoftKey);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);

                JObject jObject = JObject.Parse(response.Content);

                if (jObject["success"].ToString().Contains("alse"))
                {
                    return GetCurrentProxyTinsoft(tinsoftKey);
                }


                return jObject["proxy"].ToString();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Get Proxy Tinsoft error: " + ex.Message, "Error");
            }

            return "";
        }


        public static string GetCurrentProxyTinsoft(string tinsoftKey)
        {
            try
            {
                var client = new RestClient("http://proxy.tinsoftsv.com/api/getProxy.php?key=" + tinsoftKey);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);

                JObject jObject = JObject.Parse(response.Content);

                if (jObject["success"].ToString().Contains("alse"))
                {
                    return jObject.ToString();
                }


                return jObject["proxy"].ToString();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Get Proxy Tinsoft error: " + ex.Message, "Error");
            }

            return "";
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.ToString().StartsWith("192."))
                {
                    return ip.ToString();
                }
            }

            return "";
        }



        public static void ChangeIPXProxy(string proxy)
        {
            try
            {
                string xproxy = File.ReadAllText(Directory.GetCurrentDirectory() + "\\xproxy").Trim();
                var client = new RestClient(xproxy + proxy.Trim());
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Connection", "keep-alive");
                request.AddHeader("Cache-Control", "max-age=0");
                request.AddHeader("DNT", "1");
                request.AddHeader("Upgrade-Insecure-Requests", "1");
                //client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.85 Safari/537.36";
                request.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                request.AddHeader("Accept-Language", "en,en-US;q=0.9,vi;q=0.8");
                request.AddHeader("Cookie", "Admin-Token=admin-token; sidebarStatus=0");
                IRestResponse response = client.Execute(request);
            }
            catch { }

        }

        public static string GetIPTMProxy(string apikey)
        {
            try
            {
                string sign = CreateMD5(apikey);

                var client = new RestClient("https://tmproxy.com/api/proxy/get-new-proxy");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("accept", "application/json");
                request.AddHeader("Content-Type", "application/json");
                var body = @"{""api_key"":""" + apikey + @""",""sign"":""" + sign + @""",""id_location"":9}";
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                var jsonResult = (JObject)JsonConvert.DeserializeObject(response.Content);
                string new_proxy = jsonResult["data"]["https"].ToString();

                if (new_proxy.Equals(""))
                    return jsonResult["message"].ToString();
                else
                    return new_proxy;

            }
            catch { }

            return "";
        }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
