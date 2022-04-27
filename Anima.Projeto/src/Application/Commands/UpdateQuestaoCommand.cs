using Anima.Projeto.Application.Common;
using Anima.Projeto.Application.Requests;
using Anima.Projeto.Application.Responses;
using Anima.Projeto.Domain.Core.Entities;
using Anima.Projeto.Domain.Shared.Interfaces;
using System;
using System.Linq;

namespace Anima.Projeto.Application.Commands
{
    // os comandos são instruçoes que alteram o estado do servidor
    public class UpdateQuestaoCommand : Command<UpdateQuestaoRequest, UpdateQuestaoResponse>
    {
        public UpdateQuestaoCommand(IWriteRepository repository) : base(repository)
        {
        }

        //todo metodo handle tem que caracterizar um transação
        protected override UpdateQuestaoResponse Changes(UpdateQuestaoRequest request)
        {

            Questao questao = _repository.AsQueryable<Questao>().FirstOrDefault(x => x.Id == request.getId());

            var response = new UpdateQuestaoResponse();

            if (questao == null)
            {
                response.AddError("Questão não encontrada");
                return response;
            }


            questao.Enunciado = request.Enunciado;
            
            return new UpdateQuestaoResponse
            {
                Id = questao.Id,
                UpdatedAt = questao.UpdatedAt
            };
        }
    }
}
