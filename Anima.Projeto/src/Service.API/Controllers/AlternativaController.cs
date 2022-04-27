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
    public class AlternativaController : ControllerBase
    {
        public IWriteRepository _wrepository;
        public IReadRepository _rrepository;

        public AlternativaController(IWriteRepository wrepository, IReadRepository rrepository)
        {
            _wrepository = wrepository;
            _rrepository = rrepository;
        }


        [HttpPost]
        [Authorize(Roles = "Professor")]
        public IActionResult Add([FromBody] AddAlternativaRequest request)
        {
            
            var cmd = new AddAlternativaCommand(_wrepository);

            var response = cmd.Handle(request);

            return response.IsSuccess ? Created("", response) : NotFound(new { errors = response.Errors });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Professor")]
        public IActionResult UpdateAlternativa(Guid id, UpdateAlternativaRequest request)
        {
            var cmd = new UpdateAlternativaCommand(_wrepository);
            request.setId(id);
            var response = cmd.Handle(request);

            return response.IsSuccess ? Created("", response) : NotFound(new { errors = response.Errors });
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetById([FromRoute] Guid id)
        {

            var query = new GetAlternativaByIdQuery(_rrepository);

            var request = new GetAlternativaByIdRequest() { Id = id };

            var response = query.Handle(request);

            return response.IsSuccess ? Ok(response) : NotFound(new { errors = response.Errors });

        }
    }
}