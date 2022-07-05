using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicalRegistrationSystem.Dtos;
using VehicalRegistrationSystem.Model;
using VehicalRegistrationSystem.Services.Vehical;
using VehicleRegistrationSystem.Dtos;

namespace VehicalRegistrationSystem.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Vehicle")]
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService VehicleService)
        {
            _vehicleService = VehicleService;
        }

        [HttpGet]
        [Route("GETALL")]
       // [ValidateAntiForgeryToken]
        public async Task<ActionResult<ServiceResponse<List<GetVehicleDto>>>> GETall()
        {
            return Ok(await _vehicleService.GetAllVehical());
           
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetVehicleDto>>>> GetById(int id)
        {
            return Ok(await _vehicleService.GetVehicleByID(id));

        }

        [HttpPost]
        
        [Route("AddVehicle")]
        public async Task<ActionResult<ServiceResponse<List<GetVehicleDto>>>> AddVehicle(AddVehicleDto newVehicle)
        {
            return Ok(await _vehicleService.AddVehicle(newVehicle));

        }


        [HttpPut]
        [Route("UpdateVehicle")]
       // [Route("{id}")]

        public async Task<ActionResult<ServiceResponse<GetVehicleDto>>> UpdateVehicle(ServiceResponse<UpdateVehicleDto> newVehicle)
        {
            return Ok(await _vehicleService.UpdateVehicle(newVehicle));

        }

        [HttpDelete]
        //  [Route("DeleteVehicle")]
        [Route("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetVehicleDto>>>> DeleteVehicle(int id)
        {
            return Ok(await _vehicleService.DeleteVehicle(id));

        }
    }
}
