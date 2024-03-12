namespace RekeningRijden.RabbitMq
{
    public class StatusDTO
    {
        public string VehicleID { get; set; } = string.Empty;

        public int Status { get; set; } = 0;

        public StatusDTO() 
        { 
        
        
        }
    }
}
