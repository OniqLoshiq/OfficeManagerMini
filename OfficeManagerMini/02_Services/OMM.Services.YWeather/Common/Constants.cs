namespace OMM.Services.YWeather.Common
{
    public static class Constants
    {
        public const string YW_URL = "https://weather-ydn-yql.media.yahoo.com/forecastrss";
       
        public const string YW_OAUTH_VERSION = "1.0";

        public const string YW_AUTH_SIGNMETHOD = "HMAC-SHA1";

        public const string YW_WEATHER_ID = "woeid=839722";  // Sofia, Bulgaira

        public const string YW_UNIT_ID = "u=c";           // Metric units

        public const string YW_FORMAT = "json";
    }
}
