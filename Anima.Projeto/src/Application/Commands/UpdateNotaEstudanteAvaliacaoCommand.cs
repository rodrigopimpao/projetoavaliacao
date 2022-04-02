using Anima.Projeto.Application.Common;
using Anima.Projeto.Application.Requests;
using Anima.Projeto.Application.Responses;
using Anima.Projeto.Domain.Core.Entities;
using Anima.Projeto.Domain.Shared.Interfaces;
using System.Linq;

namespace Anima.Projeto.Application.Commands
{
    // os comandos são instruçoes que alteram o estado do servidor
    public class UpdateNotaEstudanteAvaliacaoCommand : Command<UpdateNotaEstudanteAvaliacaoRequest, UpdateNotaEstudanteAvaliacaoResponse>
    {
        public UpdateNotaEstudanteAvaliacaoCommand(IWriteRepository repository) : base(repository)
        {
        }

        //todo metodo handle tem que caracterizar um transação
        protected override UpdateNotaEstudanteAvaliacaoResponse Changes(UpdateNotaEstudanteAvaliacaoRequest request)
        {

            Nota nota = _repository.AsQueryable<Nota>().FirstOrDefault(x => x.UsuarioId == request.getUsuarioId() && x.AvaliacaoId == request.getAvaliacaoId());

            var response = new UpdateNotaEstudanteAvaliacaoResponse();

            if (nota == null)
            {
                response.AddError("Nota não encontrada");
                return response;
            }


            nota.Valor = request.Valor;
            
            return new UpdateNotaEstudanteAvaliacaoResponse
            {
                Id = nota.Id,
                UpdatedAt = nota.UpdatedAt
            };
        }
    }
}
