using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherBot.Services.Wit
{
    public class WitResponse
    {
        public string _text { get; set; }

        public WitEntities entities { get; set; }

        public string msg_id { get; set; }
    }
}