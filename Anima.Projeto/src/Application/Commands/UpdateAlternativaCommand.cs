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
    public class UpdateAlternativaCommand : Command<UpdateAlternativaRequest, UpdateAlternativaResponse>
    {
        public UpdateAlternativaCommand(IWriteRepository repository) : base(repository)
        {
        }

        //todo metodo handle tem que caracterizar um transação
        protected override UpdateAlternativaResponse Changes(UpdateAlternativaRequest request)
        {

            Alternativa alternativa = _repository.AsQueryable<Alternativa>().FirstOrDefault(x => x.Id == request.getId());

            var response = new UpdateAlternativaResponse();

            if (alternativa == null)
            {
                response.AddError("Questão não encontrada");
                return response;
            }


            alternativa.Descricao = request.Descricao;
            alternativa.Correta = request.Correta;
            
            return new UpdateAlternativaResponse
            {
                Id = alternativa.Id,
                UpdatedAt = alternativa.UpdatedAt
            };
        }
    }
}
