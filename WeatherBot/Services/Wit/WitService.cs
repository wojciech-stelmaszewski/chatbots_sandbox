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

        private readonly string _accessToken;

        public WitService(string accessToken)
        {
            _accessToken = accessToken;
        }

        public WitResponse GetResponse(string message)
        {
            try
            {
                string url = String.Concat(RequestUrl, message);
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.Headers.Add("Authorization", $"Bearer {_accessToken}");
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