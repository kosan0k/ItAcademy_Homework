using System.Threading.Tasks;
using It_AcademyHomework.Authentication;
using It_AcademyHomework.Authentication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace It_AcademyHomework.JwtExample.Controllers
{
    public class CustomerController : Controller
    {
        [Route("api/[controller]")]
        [ApiController]
        public class CustomersController : ControllerBase
        {
            private readonly IAuthenticationService customerService;
            public CustomersController(IAuthenticationService customerService)
            {
                this.customerService = customerService;
            }

            [HttpPost]
            [Route("login")]
            public async Task<IActionResult> Login(LoginRequest loginRequest)
            {
                if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Username) || string.IsNullOrEmpty(loginRequest.Password))
                {
                    return BadRequest("Missing login details");
                }
                var loginResponse = await customerService.Login(loginRequest);
                if (loginResponse == null)
                {
                    return BadRequest($"Invalid credentials");
                }
                return Ok(loginResponse);
            }
        }
    }
}
