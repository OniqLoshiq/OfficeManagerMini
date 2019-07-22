namespace OMM.Services.YWeather.YWeather
{
    public class Location
    {
        public int Woeid { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public float Lat { get; set; }
        public float _long { get; set; }
        public string Timezone_Id { get; set; }
    }
}
