using Anima.Projeto.Application.Common;
using Anima.Projeto.Application.Requests;
using Anima.Projeto.Application.Responses;
using Anima.Projeto.Domain.Core.Entities;
using Anima.Projeto.Domain.Shared.Interfaces;

namespace Anima.Projeto.Application.Commands
{
    // os comandos são instruçoes que alteram o estado do servidor
    public class AddAlternativaCommand : Command<AddAlternativaRequest, AddAlternativaResponse>
    {
        public AddAlternativaCommand(IWriteRepository repository) : base(repository)
        {
        }

        //todo metodo handle tem que caracterizar um transação
        protected override AddAlternativaResponse Changes(AddAlternativaRequest request)
        {
            var alternativa = new Alternativa(request.Descricao, request.Correta, request.QuestaoId);

            _repository.Add(alternativa);

            return new AddAlternativaResponse
            {
                Id = alternativa.Id,
                CreatedAt = alternativa.CreatedAt
            };
        }
    }
}
