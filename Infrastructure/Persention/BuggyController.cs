using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persention
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController :ControllerBase
    {

        [HttpGet("notfound")]
        public IActionResult GetNotFoundRequest()
        {
            return NotFound(); // 404
        }


        [HttpGet("servererror")]
        public IActionResult GetServerError()
        {
            throw new Exception();
            return Ok();
        }

        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(); //400
        }

        [HttpGet("badrequest/{id}")]
        public IActionResult GetBadRequest(int  id)  // validation error
        {
            return BadRequest(); // 
        }

        [HttpGet("unauthorized")]
        public IActionResult GetUnauthorizedrequest()
        {
            return Unauthorized(); //401
        }

    }
}
