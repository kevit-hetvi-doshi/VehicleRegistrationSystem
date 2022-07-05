using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using VehicalRegistrationSystem.Data;
using VehicalRegistrationSystem.Dtos;
using VehicalRegistrationSystem.Model;
using VehicleRegistrationSystem.Dtos;

namespace VehicalRegistrationSystem.Services.Vehical
{
    public class VehicleService : IVehicleService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VehicleService(DataContext context , IMapper mapper , IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetOwnerId()
        {
            return int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        public async Task<ServiceResponse<List<GetVehicleDto>>> AddVehicle(AddVehicleDto newVehicle)
        {
            var serviceResponce = new ServiceResponse<List<GetVehicleDto>>();
            var vehicle = _mapper.Map<Vehicle>(newVehicle);

            vehicle.owner = await _context.Owners.FirstOrDefaultAsync(x => x.Id == GetOwnerId());

            _context.vehicles.Add(vehicle);
            _context.SaveChanges();

            serviceResponce.Data = await  _context.vehicles
                .Where(x => x.owner.Id == GetOwnerId())
                .Select(c => _mapper.Map<GetVehicleDto>(c)).ToListAsync();

            return serviceResponce;


        }

        public async Task<ServiceResponse<List<GetVehicleDto>>> DeleteVehicle(int id)
        {
            var serviceresponce = new ServiceResponse<List<GetVehicleDto>>();

            try
            {

                var Vehicle = await _context.vehicles.FirstOrDefaultAsync(x => x.id == id && x.owner.Id == GetOwnerId());

                if (Vehicle == null)
                {

                    serviceresponce.Success = false;
                    serviceresponce.message = "Vehicle not found !!!";
                    return serviceresponce;


                }
                else
                {

                    _context.vehicles.Remove(Vehicle);


                    _context.SaveChanges();
                    serviceresponce.Data = await _context.vehicles
                        .Where(c => c.owner.Id == GetOwnerId())
                        .Select(c => _mapper.Map<GetVehicleDto>(c)).ToListAsync();

                }


            }



            catch (Exception e)
            {
                serviceresponce.Success = false;
                serviceresponce.message = e.Message;

            }
            return serviceresponce;
        }

        public async Task<ServiceResponse<List<GetVehicleDto>>> GetAllVehical()
        {
            var responce = new ServiceResponse<List<GetVehicleDto>>();

            var dbvehicle = await _context.vehicles.Where(c => c.owner.Id == GetOwnerId()).ToListAsync();

            responce.Data = dbvehicle.Select(c => _mapper.Map<GetVehicleDto>(c)).ToList();

            return responce;
        }

        public async Task<ServiceResponse<GetVehicleDto>> GetVehicleByID(int id)
        {
            var responce = new ServiceResponse<GetVehicleDto>();

            try
            {
                var vehicle = await _context.vehicles.FirstOrDefaultAsync(c => c.id == id && c.owner.Id == GetOwnerId());

                if(vehicle == null)
                {
                    responce.Success = false;
                    responce.message = "Vehicle not found !!!!";
                    return responce;
                }

                responce.Data = _mapper.Map<GetVehicleDto>(vehicle);
                return responce; 

            }

            catch(Exception e)
            {
                responce.Success = false; 
                responce.message = e.Message;
            }

            return responce;
        }

        public async Task<ServiceResponse<GetVehicleDto>> UpdateVehicle(ServiceResponse<UpdateVehicleDto> updateVehicle)
        {
            var serviceresponce = new ServiceResponse<GetVehicleDto>();

           try
            {

                var vehicle = await _context.vehicles
                    .Include(c => c.owner)
                    .FirstOrDefaultAsync(x => x.id == updateVehicle.Data.id );

                if ( vehicle.owner.Id == GetOwnerId())
                {
                    vehicle.name = updateVehicle.Data.name;
                    vehicle.RtoNumber = updateVehicle.Data.RtoNumber;
                    vehicle.Description = updateVehicle.Data.Description;
                    vehicle.fuel = updateVehicle.Data.fuel;


                    _context.SaveChanges();
                  serviceresponce.Data = _mapper.Map<GetVehicleDto>(vehicle);
                    //var v = _mapper.Map<GetVehicleDto>(vehicle);
                 //  return v;



            }
                else
                {

                    serviceresponce.Success = false;
                    serviceresponce.message = "Vehicle not found !!!";
                  return serviceresponce;
                 //  return _mapper.Map<GetVehicleDto>(vehicle);

                }

            


             }



            catch (Exception e)
             {
              serviceresponce.Success = false;
               serviceresponce.message = e.Message;

             }
           return serviceresponce;

        }
    }
}

