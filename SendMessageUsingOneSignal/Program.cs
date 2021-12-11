using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace SendMessageUsingOneSignal
{
    class Program
    {
        static void Main(string[] args)
        {
            OneSignalSender();
        }
        private static void OneSignalSender()
        {
            string oneSignalAppId = "e5a17073-4300-4bd4-84be-0dfa65809232";
            string oneSignalRestId = "NTdmMmUyZTktMjJkZi00MzhhLTljY2ItNDRkOTdhMTQwNDY4";

            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;
            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
            request.Headers.Add("authorization", "Basic " + oneSignalRestId);
            var contentsMessage = new { en = "Welcome to Cat Facts!" };
            var message = new
            {
                app_id = "e5a17073-4300-4bd4-84be-0dfa65809232",
                name = "Test",
                sms_from = "+13515296697",
                contents = new { en = "Welcome to Cat Facts!" },
                include_phone_numbers = new string[] { "+919408438517" }
            };
            var json = JsonConvert.SerializeObject(message);
            byte[] byteArray = Encoding.UTF8.GetBytes(json);
            string responseContent = null;
            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
            }
            System.Diagnostics.Debug.WriteLine(responseContent);
        }
    }
}
