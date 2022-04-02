using System.Collections.Generic;
using System.Linq;
using Anima.Projeto.Application.Common;
using Anima.Projeto.Application.Requests;
using Anima.Projeto.Domain.Core.Entities;
using Anima.Projeto.Domain.Shared.Interfaces;

namespace Anima.Projeto.Application.Queries
{
    public class GetCustomerByIdQuery : Query<GetCustomerByIdRequest, GetCustomerByIdResponse>
    {
        public GetCustomerByIdQuery(IReadRepository repository) : base(repository)
        {
        }
        public override GetCustomerByIdResponse Handle(GetCustomerByIdRequest request)
        {



            Customer customer = _repository.AsQueryable<Customer>().SingleOrDefault(x => x.Id == request.Id);

           
            if (customer == null) return null;

            
            return new GetCustomerByIdResponse
            {
                Id = customer.Id,
                CreatedAt = customer.CreatedAt,
                Email = customer.Email,
                IsActive = customer.IsActive,
                Name = customer.Name,
                UpdatedAt = customer.UpdatedAt
            };

            //return null;

        }
    }
}
