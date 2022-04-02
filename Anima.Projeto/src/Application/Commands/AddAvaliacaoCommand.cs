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
    public class AddAvaliacaoCommand : Command<AddAvaliacaoRequest, AddAvaliacaoResponse>
    {
        public AddAvaliacaoCommand(IWriteRepository repository) : base(repository)
        {
        }

        //todo metodo handle tem que caracterizar um transação
        protected override AddAvaliacaoResponse Changes(AddAvaliacaoRequest request)
        {
            var avaliacao = new Avaliacao(request.Nome, request.Descricao);

            _repository.Add(avaliacao);

            return new AddAvaliacaoResponse
            {
                Id = avaliacao.Id,
                CreatedAt = avaliacao.CreatedAt
            };
        }
    }
}
