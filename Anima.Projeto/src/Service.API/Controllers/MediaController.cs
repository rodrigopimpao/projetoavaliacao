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
    public class MediaController : ControllerBase
    {
        public IWriteRepository _wrepository;
        public IReadRepository _rrepository;

        public MediaController(IWriteRepository wrepository, IReadRepository rrepository)
        {
            _wrepository = wrepository;
            _rrepository = rrepository;
        }


        [HttpPost]
        [Authorize(Roles = "Professor")]
        public IActionResult Add([FromBody] AddMediaRequest request)
        {
            
            var cmd = new AddMediaCommand(_wrepository);

            var response = cmd.Handle(request);

            return response.IsSuccess ? Created("", response) : NotFound(new { errors = response.Errors });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Professor")]
        public IActionResult Update(Guid id, UpdateMediaRequest request)
        {
            var cmd = new UpdateMediaCommand(_wrepository);
            request.setId(id);
            var response = cmd.Handle(request);

            return response.IsSuccess ? Created("", response) : NotFound(new { errors = response.Errors });
        }

        [HttpPut("estudante/{id}")]
        [Authorize(Roles = "Professor")]
        public IActionResult UpdateMediaEstudante(Guid id, UpdateMediaEstudanteRequest request)
        {
            var cmd = new UpdateMediaEstudanteCommand(_wrepository);
            request.setUsuarioId(id);
            var response = cmd.Handle(request);

            return response.IsSuccess ? Created("", response) : NotFound(new { errors = response.Errors });
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetById([FromRoute] Guid id)
        {

            var query = new GetMediaByIdQuery(_rrepository);

            var request = new GetMediaByIdRequest() { Id = id };

            var response = query.Handle(request);

            return response.IsSuccess ? Ok(response) : BadRequest(new { errors = response.Errors });
        }

        [HttpGet("estudante/{id}")]
        [Authorize]
        public IActionResult GetMediaEstudanteById([FromRoute] Guid id)
        {

            var query = new GetMediaByEstudanteIdQuery(_rrepository);

            var request = new GetMediaByIdRequest() { EstudanteId = id };

            var response = query.Handle(request);

            return response.IsSuccess ? Ok(response) : BadRequest(new { errors = response.Errors });
        }
    }
}