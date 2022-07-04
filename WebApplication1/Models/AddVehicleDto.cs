namespace WebApplication1.Models
{
    public class AddVehicleDto
    {
        public string name { get; set; }

        public string RtoNumber { get; set; }

        public string Description { get; set; }

        public Fuel fuel { get; set; } = Fuel.Petrol;

    }
}
