using Microsoft.EntityFrameworkCore;
using VehicalRegistrationSystem.Model;
using VehicleRegistrationSystem.Model;

namespace VehicalRegistrationSystem.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

       


       public DbSet<Vehicle> vehicles { get; set; }

       public DbSet<Owner> Owners { get; set; }
    }
}
