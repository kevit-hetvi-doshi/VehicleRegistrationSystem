using Microsoft.AspNetCore.Mvc;
using VehicalRegistrationSystem.Model;
using VehicleRegistrationSystem.Dtos;
using VehicleRegistrationSystem.Services.Owner;

namespace VehicleRegistrationSystem.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class OwnerController : Controller
    {
        private readonly IOwnerService _ownerService;

        public OwnerController(IOwnerService  OwnerService)
        {
            _ownerService = OwnerService;
        }

        [HttpPost]
        [Route("Register")]

        public async Task<ActionResult<ServiceResponse<int>>> Register(OwnerRegisterDto newOwner)
        {
            return Ok ( await _ownerService.Register(newOwner));
        }

        [HttpPost]
        [Route("Login")]

        public async Task<ActionResult<ServiceResponse<int>>> Login(OwnerLogInDto newOwner)
        {
            return Ok(await _ownerService.LogIn(newOwner.Email, newOwner.password));
        }
    }
}
