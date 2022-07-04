using VehicalRegistrationSystem.Model;
using VehicleRegistrationSystem.Dtos;

namespace VehicleRegistrationSystem.Services.Owner
{
    public interface IOwnerService
    {
        Task<ServiceResponse<int>> Register(OwnerRegisterDto Owner);

        Task<ServiceResponse<String>> LogIn(String email , string password);

        Task<bool> UserExist(String Email);
    }
}
