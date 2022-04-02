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
    public class AddQuestaoCommand : Command<AddQuestaoRequest, AddQuestaoResponse>
    {
        public AddQuestaoCommand(IWriteRepository repository) : base(repository)
        {
        }

        //todo metodo handle tem que caracterizar um transação
        protected override AddQuestaoResponse Changes(AddQuestaoRequest request)
        {
            var questao = new Questao(request.Enunciado, request.AvaliacaoId);

            _repository.Add(questao);

            return new AddQuestaoResponse
            {
                Id = questao.Id,
                CreatedAt = questao.CreatedAt
            };
        }
    }
}
