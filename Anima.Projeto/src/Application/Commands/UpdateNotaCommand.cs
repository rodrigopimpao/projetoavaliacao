using Anima.Projeto.Application.Common;
using Anima.Projeto.Application.Requests;
using Anima.Projeto.Application.Responses;
using Anima.Projeto.Domain.Core.Entities;
using Anima.Projeto.Domain.Shared.Interfaces;
using System.Linq;

namespace Anima.Projeto.Application.Commands
{
    // os comandos são instruçoes que alteram o estado do servidor
    public class UpdateNotaCommand : Command<UpdateNotaRequest, UpdateNotaResponse>
    {
        public UpdateNotaCommand(IWriteRepository repository) : base(repository)
        {
        }

        //todo metodo handle tem que caracterizar um transação
        protected override UpdateNotaResponse Changes(UpdateNotaRequest request)
        {

            Nota nota = _repository.AsQueryable<Nota>().FirstOrDefault(x => x.Id == request.getId());

            nota.Valor = request.Valor;
            
            return new UpdateNotaResponse
            {
                Id = nota.Id,
                UpdatedAt = nota.UpdatedAt
            };
        }
    }
}
