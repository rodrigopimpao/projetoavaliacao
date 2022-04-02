using Anima.Projeto.Application.Queries;
using Anima.Projeto.Application.Requests;
using Anima.Projeto.Domain.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Anima.Projeto.Service.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        public IWriteRepository _wrepository;
        public IReadRepository _rrepository;

        public LoginController(IWriteRepository wrepository, IReadRepository rrepository)
        {
            _wrepository = wrepository;
            _rrepository = rrepository;
        }

        [HttpPost("login")]
        public IActionResult Login(GetUsuarioByLoginRequest request)
        {
            var query = new GetUsuarioByLoginQuery(_rrepository);

            var response = query.Handle(request);

            return response.IsSuccess ? Ok(response) : BadRequest(new { errors = response.Errors });
        }

    }
}