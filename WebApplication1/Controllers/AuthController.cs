using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            ResponseModel<string> response = new ResponseModel<string>();

            var token = await _authService.AuthenticateAsync(request.Username, request.Password);

            if (token == null)
            {

                response.Status = false;
                response.Message = "Unauthorized";
                response.Errors.Add("Usuario o contraseña no validos.");

                return Unauthorized(response);
            }

            response.Status = true;
            response.Message = "Successfully";
            response.Data = token;

            return Ok(response);
        }

    }
}
