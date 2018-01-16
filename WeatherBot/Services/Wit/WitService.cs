using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace WeatherBot.Services.Wit
{
    public class WitService
    {
        private const string RequestUrl = "https://api.wit.ai/message?v=20180116&q=";
        

        public WitResponse GetResponse(string message)
        {
            try
            {
                string url = String.Concat(RequestUrl, message);
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.Headers.Add("Authorization", "Bearer E6DFDKGS4STETYSJ7DVS6YASQFZNKTPX");
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                return JsonConvert.DeserializeObject<WitResponse>(responseString);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}