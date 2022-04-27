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
    public class UpdateAvaliacaoCommand : Command<UpdateAvaliacaoRequest, UpdateAvaliacaoResponse>
    {
        public UpdateAvaliacaoCommand(IWriteRepository repository) : base(repository)
        {
        }

        //todo metodo handle tem que caracterizar um transação
        protected override UpdateAvaliacaoResponse Changes(UpdateAvaliacaoRequest request)
        {

            Avaliacao avaliacao = _repository.AsQueryable<Avaliacao>().FirstOrDefault(x => x.Id == request.getId());

            var response = new UpdateAvaliacaoResponse();

            if (avaliacao == null)
            {
                response.AddError("Avaliação não encontrada");
                return response;
            }


            avaliacao.Descricao = request.Descricao;
            avaliacao.Nome = request.Nome;

            return new UpdateAvaliacaoResponse
            {
                Id = avaliacao.Id,
                UpdatedAt = avaliacao.UpdatedAt
            };
        }
    }
}
