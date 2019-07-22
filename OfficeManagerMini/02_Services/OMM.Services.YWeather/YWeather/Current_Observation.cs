namespace OMM.Services.YWeather.YWeather
{
    public class Current_Observation
    {
        public Wind Wind { get; set; }
        public Atmosphere Atmosphere { get; set; }
        public Astronomy Astronomy { get; set; }
        public Condition Condition { get; set; }
        public int PubDate { get; set; }
    }
}
