using System;
using Anima.Projeto.Application.Queries;
using Anima.Projeto.Application.Requests;
using Anima.Projeto.Domain.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Anima.Projeto.Service.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        public IWriteRepository _wrepository;
        public IReadRepository _rrepository;

        public ProfessorController(IWriteRepository wrepository, IReadRepository rrepository)
        {
            _wrepository = wrepository;
            _rrepository = rrepository;
        }

       
        [HttpGet("{id}")]        
        public IActionResult GetById([FromRoute] Guid id)
        {

            var query = new GetProfessorByIdQuery(_rrepository);

            var request = new GetUsuarioByIdRequest() { Id = id };

            var response = query.Handle(request);

            return response.IsSuccess ? Ok(response) : BadRequest(new { errors = response.Errors });
        }
    }
}