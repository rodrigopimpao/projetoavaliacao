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
    public class UpdateMediaCommand : Command<UpdateMediaRequest, UpdateMediaResponse>
    {
        public UpdateMediaCommand(IWriteRepository repository) : base(repository)
        {
        }

        //todo metodo handle tem que caracterizar um transação
        protected override UpdateMediaResponse Changes(UpdateMediaRequest request)
        {

            Media media = _repository.AsQueryable<Media>().FirstOrDefault(x => x.Id == request.getId());

            var response = new UpdateMediaResponse();

            if (media == null)
            {
                response.AddError("Media não encontrada");
                return response;
            }

            media.Total = request.Total;
            
            return new UpdateMediaResponse
            {
                Id = media.Id,
                UpdatedAt = media.UpdatedAt
            };
        }
    }
}
