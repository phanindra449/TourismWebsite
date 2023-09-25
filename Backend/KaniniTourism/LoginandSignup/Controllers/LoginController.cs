using LoginandSignup.Interfaces;
using LoginandSignup.Models;
using LoginandSignup.Models.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace LoginandSignup.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("CORS")]

    public class LoginController : ControllerBase
    {
        private readonly IManageUser _manageUser;



        public LoginController(IManageUser manageUser)
        {
            _manageUser = manageUser;
        }




        [HttpPost]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDTO>> Login(UserDTO userDTO)
        {
            var result = await _manageUser.Login(userDTO);
            if (result != null)
                return Ok(result);
            return BadRequest("Invalid credentials");

        }

      
    }
}
