using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherBot.Services.Wit
{
    public class WitIntent
    {
        public decimal confidence { get; set; }
        public string value { get; set; }
    }
}