using VehicalRegistrationSystem.Model;

namespace VehicleRegistrationSystem.Model
{
    public class Owner
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public long phonenumber { get; set; }

        public string address { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] passwordSalt { get; set; } 



        public List<Vehicle> vehicles { get; set; }

    }
}
