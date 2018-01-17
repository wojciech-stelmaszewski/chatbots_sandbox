using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeatherBot.Geo;

namespace WeatherBot.Weather
{
    public class WeatherLocation
    {
        public string Name { get; set; }

        public GeoLocation GeoLocation { get; set; }
    }
}