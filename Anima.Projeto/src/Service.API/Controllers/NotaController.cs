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
    public class NotaController : ControllerBase
    {
        public IWriteRepository _wrepository;
        public IReadRepository _rrepository;

        public NotaController(IWriteRepository wrepository, IReadRepository rrepository)
        {
            _wrepository = wrepository;
            _rrepository = rrepository;
        }


        [HttpPost]
        [Authorize]
        public IActionResult Add([FromBody] AddNotaRequest request)
        {
            
            var cmd = new AddNotaCommand(_wrepository);

            var response = cmd.Handle(request);

            return response.IsSuccess ? Created("", response) : NotFound(new { errors = response.Errors });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Professor")]
        public IActionResult Update(Guid id, UpdateNotaRequest request)
        {
            var cmd = new UpdateNotaCommand(_wrepository);
            request.setId(id);
            var response = cmd.Handle(request);

            return response.IsSuccess ? Created("", response) : NotFound(new { errors = response.Errors });
        }

        [HttpPut("{usuarioId}/{avaliacaoId}")]
        [Authorize(Roles = "Professor")]
        public IActionResult UpdateByEstudanteAvaliacao(Guid usuarioId, Guid avaliacaoId, UpdateNotaEstudanteAvaliacaoRequest request)
        {
            var cmd = new UpdateNotaEstudanteAvaliacaoCommand(_wrepository);
            request.setUsuarioId(usuarioId);
            request.setAvaliacaoId(avaliacaoId);
            var response = cmd.Handle(request);

            return response.IsSuccess ? Created("", response) : NotFound(new { errors = response.Errors });
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetById([FromRoute] Guid id)
        {

            var query = new GetNotaByIdQuery(_rrepository);

            var request = new GetNotaByIdRequest() { Id = id };

            var response = query.Handle(request);

            return response.IsSuccess ? Ok(response) : BadRequest(new { errors = response.Errors });
        }

        [HttpGet("estudante/{id}")]
        [Authorize]
        public IActionResult GetNotaEstudanteById([FromRoute] Guid id)
        {

            var query = new GetNotaByEstudanteIdQuery(_rrepository);

            var request = new GetNotaByIdRequest() { EstudanteId = id };

            var response = query.Handle(request);

            return response.IsSuccess ? Ok(response) : BadRequest(new { errors = response.Errors });
        }
    }
}