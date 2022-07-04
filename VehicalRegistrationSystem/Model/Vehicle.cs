using System.ComponentModel.DataAnnotations;
using VehicleRegistrationSystem.Model;

namespace VehicalRegistrationSystem.Model
{
    public class Vehicle
    {
        public int id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string RtoNumber { get; set; }

        
        public string Description { get; set; }

        public Fuel fuel { get; set; } = Fuel.Petrol;

        //public int OwnerId { get; set; }

        public Owner owner { get; set; }
    }
}
