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
    public class AvaliacaoController : ControllerBase
    {
        public IWriteRepository _wrepository;
        public IReadRepository _rrepository;

        public AvaliacaoController(IWriteRepository wrepository, IReadRepository rrepository)
        {
            _wrepository = wrepository;
            _rrepository = rrepository;
        }


        [HttpPost]
        [Authorize(Roles = "Professor")]
        public IActionResult Add([FromBody] AddAvaliacaoRequest request)
        {
            var cmd = new AddAvaliacaoCommand(_wrepository);

            var response = cmd.Handle(request);

            return response.IsSuccess ? Created("", response) : NotFound(new { errors = response.Errors });
        }

        [HttpGet]
        [Authorize(Roles = "Professor")]
        public IActionResult GetList()
        {

            var query = new GetAvaliacaoListQuery(_rrepository);

            var request = new GetAvaliacaoByIdRequest();

            var response = query.Handle(request);

            return response.IsSuccess ? Ok(response) : BadRequest(new { errors = response.Errors });
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetById([FromRoute] Guid id)
        {

            var query = new GetAvaliacaoByIdQuery(_rrepository);

            var request = new GetAvaliacaoByIdRequest() { Id = id };

            var response = query.Handle(request);

            return response.IsSuccess ? Ok(response) : NotFound(new { errors = response.Errors });

        }
    }
}