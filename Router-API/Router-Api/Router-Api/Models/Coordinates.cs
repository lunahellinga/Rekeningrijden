namespace Router_Api.Models
{
    public class Coordinates
    {
        public Guid Id { get; set; }

        public string VehicleId { get; set; }

        public double Lat { get; set; }

        public double Long { get; set; }

        public DateTime Time { get; set; }

        public Coordinates() 
        { 
            
        }    


    }
}
