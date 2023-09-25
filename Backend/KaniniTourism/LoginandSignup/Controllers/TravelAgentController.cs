using LoginandRegistration.Models.DTO;
using LoginandSignup.Interfaces;
using LoginandSignup.Models;
using LoginandSignup.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LoginandSignup.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("CORS")]

    public class TravelAgentController : ControllerBase
    {
        private readonly IManageTravelAgent _managetravelagent;
        private readonly IRepo<TravelAgent, int> _travelagentrepo;
        public TravelAgentController(IManageTravelAgent managetravelagent, IRepo<TravelAgent, int> travelagentrepo)
        {
            _managetravelagent = managetravelagent;
            _travelagentrepo = travelagentrepo;
        }


        [HttpPost]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDTO>> RegisterAsTravelAgent(TravelAgentDTO travelagentDTO)
        {
            try
            {
                var result = await _managetravelagent.TravelAgentRegistration(travelagentDTO);
                if (result != null)
                    return Ok(result);
                return BadRequest("Unable to register at this moment");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                // Handle or log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }

        }





        [HttpGet]
        [ProducesResponseType(typeof(List<TravelAgent>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ICollection<TravelAgent>>> GetAllTravelAgents()
        {
            try
            {
                var travelagents = await _travelagentrepo.GetAll();
                if (travelagents != null)
                {
                    return Ok(travelagents);

                }
                return NotFound("No travelagents  available");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;

        }

        [HttpPost]
        [EnableCors("CORS")]

        [ProducesResponseType(typeof(TravelAgent), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TravelAgent>> UpdateTravelAgentStatus(StatusDTO statusDTO)
        {
            if (statusDTO != null)
            {
                var result = await _managetravelagent.StatusUpdate(statusDTO);
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest("Cannot update TravelAgent status right now");
            }
            return BadRequest("Enter the credentials properly");
        }



        [HttpPost]
        [ProducesResponseType(typeof(TravelAgent), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TravelAgent>> UpdateTravelAgentDetails(TravelAgent travelagent)
        {
            if (travelagent != null)
            {
                var result = await _travelagentrepo.Update(travelagent);
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest("Cannot update TravelAgent Details right now");
            }
            return BadRequest("Enter the credentials properly");
        }
    }
}
