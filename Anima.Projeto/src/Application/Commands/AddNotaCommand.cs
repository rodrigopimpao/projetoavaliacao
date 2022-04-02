using Anima.Projeto.Application.Common;
using Anima.Projeto.Application.Requests;
using Anima.Projeto.Application.Responses;
using Anima.Projeto.Domain.Core.Entities;
using Anima.Projeto.Domain.Shared.Interfaces;

namespace Anima.Projeto.Application.Commands
{
    // os comandos são instruçoes que alteram o estado do servidor
    public class AddNotaCommand : Command<AddNotaRequest, AddNotaResponse>
    {
        public AddNotaCommand(IWriteRepository repository) : base(repository)
        {
        }

        //todo metodo handle tem que caracterizar um transação
        protected override AddNotaResponse Changes(AddNotaRequest request)
        {

            var nota = new Nota(request.Valor, request.EstudanteId, request.AvaliacaoId);

            _repository.Add(nota);

            return new AddNotaResponse
            {
                Id = nota.Id,
                CreatedAt = nota.CreatedAt
            };
        }
    }
}
