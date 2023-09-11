//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace WebAppInsurancePolicy.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class SecurityController : ControllerBase
//    {
//    }
//}

/*using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

 

namespace WebRestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
    }
}*/
using WebAppInsurancePolicy.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static WebAppInsurancePolicy.Models.SecurityService;



namespace WebRestApp.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        IConfiguration config;
        SecurityService service;
        public SecurityController(IConfiguration _config, SecurityService service)
        {
            config = _config;
            this.service = service;
        }





        [HttpPost]
        [Route("login")]
        public IActionResult Login(OperatorLoginModel model)
        {
            TokenAndRole? tokenAndRole = service.AuthenticateUserAndGetToken(model);
            if (tokenAndRole == null)
            {
                return BadRequest("Invalid UserName or Password..");
            }
            else
                return Ok(tokenAndRole);
        }
    }
}