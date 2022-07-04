using AutoMapper;
using VehicalRegistrationSystem.Dtos;
using VehicalRegistrationSystem.Model;
using VehicleRegistrationSystem.Dtos;
using VehicleRegistrationSystem.Model;

namespace VehicleRegistrationSystem
{
    public class AutoMapperProfile : Profile 
    {
        public AutoMapperProfile()
        {
            CreateMap<Vehicle, GetVehicleDto>();
            CreateMap<AddVehicleDto, Vehicle>();
            CreateMap<OwnerRegisterDto, Owner>();
            
        }

    }
}
