using Anima.Projeto.Application.Common;
using Anima.Projeto.Application.Requests;
using Anima.Projeto.Application.Responses;
using Anima.Projeto.Domain.Core.Entities;
using Anima.Projeto.Domain.Shared.Interfaces;

namespace Anima.Projeto.Application.Commands
{
    // os comandos são instruçoes que alteram o estado do servidor
    public class AddMediaCommand : Command<AddMediaRequest, AddMediaResponse>
    {
        public AddMediaCommand(IWriteRepository repository) : base(repository)
        {
        }

        //todo metodo handle tem que caracterizar um transação
        protected override AddMediaResponse Changes(AddMediaRequest request)
        {

            var media = new Media(request.Total);

            media.UsuarioId = request.UsuarioId;

            _repository.Add(media);

            return new AddMediaResponse
            {
                Id = media.Id,
                CreatedAt = media.CreatedAt
            };
        }
    }
}
