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
    public class UpdateMediaEstudanteCommand : Command<UpdateMediaEstudanteRequest, UpdateMediaEstudanteResponse>
    {
        public UpdateMediaEstudanteCommand(IWriteRepository repository) : base(repository)
        {
        }

        //todo metodo handle tem que caracterizar um transação
        protected override UpdateMediaEstudanteResponse Changes(UpdateMediaEstudanteRequest request)
        {

            Media media = _repository.AsQueryable<Media>().FirstOrDefault(x => x.UsuarioId == request.getUsuarioId());

            var response = new UpdateMediaEstudanteResponse();

            if (media == null)
            {
                response.AddError("Media não encontrada");
                return response;
            }


            media.Total = request.Total;
            
            return new UpdateMediaEstudanteResponse
            {
                Id = media.Id,
                UpdatedAt = media.UpdatedAt
            };
        }
    }
}
