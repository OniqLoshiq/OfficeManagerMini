﻿namespace OMM.Services.YWeather.YWeather
{
    public class Forecast
    {
        public string Day { get; set; }
        public long Date { get; set; }
        public int Low { get; set; }
        public int High { get; set; }
        public string Text { get; set; }
        public string Code { get; set; }
    }
}
