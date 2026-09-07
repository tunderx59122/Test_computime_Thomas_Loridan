namespace template_API.Models
{
    public class DailyTemperatureModel
    {
        public string Date { get; set; }
        public float MaxTemperature { get; set; }
        public float MinTemperature { get; set; }
        public float Range { get; set; }
    }
}
