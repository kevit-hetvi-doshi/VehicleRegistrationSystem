using VehicalRegistrationSystem.Model;

namespace VehicleRegistrationSystem.Dtos
{
    public class AddVehicleDto
    {
        public string name { get; set; }

        public string RtoNumber { get; set; }

        public string Description { get; set; }

        public Fuel fuel { get; set; } = Fuel.Petrol;

        //public int OwnerId { get; set; }
    }
}
