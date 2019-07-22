using Newtonsoft.Json;
using OMM.Services.YWeather.Common;
using OMM.Services.YWeather.YWeather;
using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OMM.Services.YWeather
{
    public class YWeatherService : IYWeatherService
    {
        private readonly string YW_CONSUMER_SECRET = Environment.GetEnvironmentVariable("YW_CONSUMER_SECRET");
        private readonly string YW_CONSUMER_KEY = Environment.GetEnvironmentVariable("YW_CONSUMER_KEY");
        private readonly string YW_APP_ID = Environment.GetEnvironmentVariable("YW_APP_ID");


        public async Task<YWeatherInfo> GetWeatherInfoAsync()
        {
            string lURL = Constants.YW_URL + "?" + Constants.YW_WEATHER_ID + "&" + Constants.YW_UNIT_ID + "&format=" + Constants.YW_FORMAT;

            var lClt = new WebClient();

            lClt.Headers.Set("Content-Type", "application/" + Constants.YW_FORMAT);
            lClt.Headers.Add("X-Yahoo-App-Id", YW_APP_ID);
            lClt.Headers.Add("Authorization", this.GetAuth());

            byte[] lDataBuffer = await lClt.DownloadDataTaskAsync(lURL);

            string serializedInfo = Encoding.ASCII.GetString(lDataBuffer);

            YWeatherInfo deserializedInfo = JsonConvert.DeserializeObject<YWeatherInfo>(serializedInfo);

            return deserializedInfo;
        }

        private string GetTimeStamp()
        {
            TimeSpan lTS = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64(lTS.TotalSeconds).ToString();
        }

        private string GetNonce()
        {
            return Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()));
        }

        private string GetAuth()
        {
            string lNonce = this.GetNonce();
            string lTimes = this.GetTimeStamp();
            string lCKey = string.Concat(YW_CONSUMER_SECRET, "&");
            string lSign = string.Format(  // note the sort order !!!
             $"format={Constants.YW_FORMAT}&" +
             $"oauth_consumer_key={YW_CONSUMER_KEY}&" +
             $"oauth_nonce={lNonce}&" +
             $"oauth_signature_method={Constants.YW_AUTH_SIGNMETHOD}&" +
             $"oauth_timestamp={lTimes}&" +
             $"oauth_version={Constants.YW_OAUTH_VERSION}&" +
             $"{Constants.YW_UNIT_ID}&{Constants.YW_WEATHER_ID}");

            lSign = string.Concat(
             "GET&", Uri.EscapeDataString(Constants.YW_URL), "&", Uri.EscapeDataString(lSign)
            );

            using (var lHasher = new HMACSHA1(Encoding.ASCII.GetBytes(lCKey)))
            {
                lSign = Convert.ToBase64String(
                 lHasher.ComputeHash(Encoding.ASCII.GetBytes(lSign))
                );
            }  // end using

            return "OAuth " +
                   "oauth_consumer_key=\"" + YW_CONSUMER_KEY + "\", " +
                   "oauth_nonce=\"" + lNonce + "\", " +
                   "oauth_timestamp=\"" + lTimes + "\", " +
                   "oauth_signature_method=\"" + Constants.YW_AUTH_SIGNMETHOD + "\", " +
                   "oauth_signature=\"" + lSign + "\", " +
                   "oauth_version=\"" + Constants.YW_OAUTH_VERSION + "\"";
        }
    }
}
