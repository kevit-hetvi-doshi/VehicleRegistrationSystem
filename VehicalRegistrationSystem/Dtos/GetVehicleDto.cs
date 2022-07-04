using VehicalRegistrationSystem.Model;

namespace VehicalRegistrationSystem.Dtos
{
    public class GetVehicleDto
    {
        public int id { get; set; }

        public string name { get; set; }

        public string RtoNumber { get; set; }

        public string Description { get; set; }

        public Fuel fuel { get; set; } = Fuel.Petrol;
    }
}
