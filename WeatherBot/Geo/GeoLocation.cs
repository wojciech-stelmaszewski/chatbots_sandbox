﻿using System;

namespace WeatherBot.Geo
{
    [Serializable]
    public class GeoLocation
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}