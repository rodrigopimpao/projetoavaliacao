using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Anima.Projeto.Application.Commands;
using Anima.Projeto.Application.Queries;
using Anima.Projeto.Application.Requests;
using Anima.Projeto.Domain.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Anima.Projeto.Service.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RespostaEstudanteController : ControllerBase
    {
        public IWriteRepository _wrepository;
        public IReadRepository _rrepository;

        public RespostaEstudanteController(IWriteRepository wrepository, IReadRepository rrepository)
        {
            _wrepository = wrepository;
            _rrepository = rrepository;
        }


        [HttpPost]
        [Authorize(Roles = "Estudante")]
        public IActionResult Add([FromBody] AddRespostaEstudanteRequest request)
        {
            var cmd = new AddRespostaEstudanteCommand(_wrepository);

            var response = cmd.Handle(request);

            return response.IsSuccess ? Created("", response) : NotFound(new { errors = response.Errors });
        }

        [HttpPut("{usuarioId}/{questaoId}")]
        [Authorize(Roles = "Estudante")]
        public IActionResult Update(Guid questaoId, Guid usuarioId, UpdateRespostaEstudanteRequest request)
        {
            var cmd = new UpdateRespostaEstudanteCommand(_wrepository);
            request.setQuestaoId(questaoId);
            request.setUsuarioId(usuarioId);

            var response = cmd.Handle(request);

            return response.IsSuccess ? Created("", response) : NotFound(new { errors = response.Errors });
        }

        [HttpGet("{estudanteId}/{questaoId}")]
        [Authorize]
        public IActionResult GetById([FromRoute] Guid estudanteId, Guid questaoId)
        {

            var query = new GetRespostaEstudanteByIdQuery(_rrepository);

            var request = new GetRespostaEstudanteByIdRequest() { EstudanteId = estudanteId,  QuestaoId = questaoId };

            var response = query.Handle(request);

            return response.IsSuccess ? Ok(response) : BadRequest(new { errors = response.Errors });
        }
    }
}