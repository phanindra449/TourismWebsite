using LoginandSignup.Interfaces;
using LoginandSignup.Models.DTO;
using LoginandSignup.Models;

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Cors;

namespace LoginandRegistration.Controllers
{


    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("CORS")]

    public class TravellerController : ControllerBase
    {
        private readonly IManageTraveller _managetravellers;
        private readonly IRepo<Traveller, int> _repo;

        public TravellerController(IManageTraveller managetravellers, IRepo<Traveller, int> repo)
        {
            _managetravellers = managetravellers;
            _repo = repo;
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<UserDTO>> RegisterAsTraveller(TravellerDTO travellerDTO)
        {
            var result = await _managetravellers.TravellerRegistration(travellerDTO);
            if (result != null)
                return Ok(result);
            return BadRequest("Unable to register at this moment");
        }



        [HttpGet]

        [ProducesResponseType(typeof(List<Traveller>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ICollection<Traveller>>> GetAllTravellers()
        {
            try
            {
                var travellers = await _repo.GetAll();
                if (travellers != null)
                {
                    return Ok(travellers);

                }
                return NotFound("No travellers  available");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }




    }
}
