using System;
using System.Collections.Generic;
using Anima.Projeto.Application.Common;
using Anima.Projeto.Application.Requests;
using Anima.Projeto.Application.Responses;
using Anima.Projeto.Domain.Core.Entities;
using Anima.Projeto.Domain.Shared.Interfaces;

namespace Anima.Projeto.Application.Commands
{
    // os comandos são instruçoes que alteram o estado do servidor
    public class AddCustomerCommand : Command<AddCustomerRequest, AddCustomerResponse>
    {
        public AddCustomerCommand(IWriteRepository repository) : base(repository)
        {
        }

        //todo metodo handle tem que caracterizar um transação
        protected override AddCustomerResponse Changes(AddCustomerRequest request)
        {
            var customer = new Customer(request.Name, request.Email);

            _repository.Add(customer);

            return new AddCustomerResponse
            {
                Id = customer.Id,
                CreatedAt = customer.CreatedAt
            };
        }
    }
}
