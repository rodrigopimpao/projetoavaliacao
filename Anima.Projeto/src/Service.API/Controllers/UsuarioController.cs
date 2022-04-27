using System;
using Anima.Projeto.Application.Commands;
using Anima.Projeto.Application.Queries;
using Anima.Projeto.Application.Requests;
using Anima.Projeto.Domain.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Anima.Projeto.Service.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        public IWriteRepository _wrepository;
        public IReadRepository _rrepository;

        public UsuarioController(IWriteRepository wrepository, IReadRepository rrepository)
        {
            _wrepository = wrepository;
            _rrepository = rrepository;
        }


        [HttpPost]
        public IActionResult Add(AddUsuarioRequest request)
        {
            var cmd = new AddUsuarioCommand(_wrepository);

            var response = cmd.Handle(request);

            return response.IsSuccess ? Created("",response) : NotFound(new { errors = response.Errors });
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Professor")]
        public IActionResult GetById([FromRoute] Guid id)
        {

            var query = new GetUsuarioByIdQuery(_rrepository);

            var request = new GetUsuarioByIdRequest() { Id = id };

            var response = query.Handle(request);

            return response.IsSuccess ? Ok(response) : BadRequest(new { errors = response.Errors });
        }

        [HttpGet]
        [Authorize(Roles = "Professor")]
        public IActionResult GetList()
        {

            var query = new GetUsuarioListQuery(_rrepository);

            var request = new GetUsuarioListRequest();

            var response = query.Handle(request);

            return response.IsSuccess ? Ok(response) : BadRequest(new { errors = response.Errors });
        }


        [HttpGet("funcao/{funcao}")]
        [Authorize]
        public IActionResult GetListFuncao([FromRoute] String funcao)
        {

            var query = new GetUsuarioListQuery(_rrepository);

            var request = new GetUsuarioListRequest() { Funcao = funcao };

            var response = query.Handle(request);

            return response.IsSuccess ? Ok(response) : BadRequest(new { errors = response.Errors });
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