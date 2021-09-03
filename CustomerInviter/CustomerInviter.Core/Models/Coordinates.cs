namespace CustomerInviter.Core.Models
{
    public class Coordinates
    {
        public Coordinates()
        {
        }

        public Coordinates(double lat, double lon)
        {
            Latitude = lat;
            Longitude = lon;
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        
    }
}