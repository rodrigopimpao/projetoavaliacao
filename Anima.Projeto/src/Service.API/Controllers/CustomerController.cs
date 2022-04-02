using System;
using Anima.Projeto.Application.Commands;
using Anima.Projeto.Application.Queries;
using Anima.Projeto.Application.Requests;
using Anima.Projeto.Domain.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Anima.Projeto.Service.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        public IWriteRepository _wrepository;

        public IReadRepository _rrepository;

        public CustomerController(IWriteRepository wrepository, IReadRepository rrepository)
        {
            _wrepository = wrepository;
            _rrepository = rrepository;
        }

        [HttpPost]
        public IActionResult Add([FromBody] AddCustomerRequest request)
        {
            var cmd = new AddCustomerCommand(_wrepository);

            var response = cmd.Handle(request);

            return response.IsSuccess ? Created("", response) : NotFound(new { errors = response.Errors });
        }

        [HttpGet("{id}")]        
        public IActionResult GetById([FromRoute] Guid id)
        {

            Console.WriteLine(id);
            var query = new GetCustomerByIdQuery(_rrepository);

            var request = new GetCustomerByIdRequest() { Id = id };

            var response = query.Handle(request);

            return response.IsSuccess ? Ok(response) : NotFound(new { errors = response.Errors });
        }
    }
}