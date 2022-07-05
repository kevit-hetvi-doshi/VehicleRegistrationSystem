using VehicalRegistrationSystem.Dtos;
using VehicalRegistrationSystem.Model;
using VehicleRegistrationSystem.Dtos;

namespace VehicalRegistrationSystem.Services.Vehical
{
    public interface IVehicleService
    {
        Task<ServiceResponse<List<GetVehicleDto>>> GetAllVehical();

        Task<ServiceResponse<GetVehicleDto>> GetVehicleByID(int id);

        Task<ServiceResponse<List<GetVehicleDto>>> AddVehicle(AddVehicleDto newVehicle);

        Task<ServiceResponse<GetVehicleDto>> UpdateVehicle(ServiceResponse<UpdateVehicleDto> updateVehicle);

        Task<ServiceResponse<List<GetVehicleDto>>> DeleteVehicle(int id);
    }
}

