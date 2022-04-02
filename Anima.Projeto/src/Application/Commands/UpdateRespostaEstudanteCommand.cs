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
    public class UpdateRespostaEstudanteCommand : Command<UpdateRespostaEstudanteRequest, UpdateRespostaEstudanteResponse>
    {
        public UpdateRespostaEstudanteCommand(IWriteRepository repository) : base(repository)
        {
        }

        //todo metodo handle tem que caracterizar um transação
        protected override UpdateRespostaEstudanteResponse Changes(UpdateRespostaEstudanteRequest request)
        {
            RespostaEstudante respostaEstudante = _repository.AsQueryable<RespostaEstudante>().FirstOrDefault(x => x.UsuarioId == request.getUsuarioId() && x.QuestaoId == request.getQuestaoId());

            var response = new UpdateRespostaEstudanteResponse();

            if (respostaEstudante == null)
            {
                response.AddError("Resposta não encontrada");
                return response;
            }

            respostaEstudante.AlternativaId = request.AlternativaId;
            
            return new UpdateRespostaEstudanteResponse
            {
                Id = respostaEstudante.Id,
                UpdatedAt = respostaEstudante.UpdatedAt
            };
        }
    }
}
