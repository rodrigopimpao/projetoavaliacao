using System.Linq;
using Anima.Projeto.Application.Common;
using Anima.Projeto.Application.Requests;
using Anima.Projeto.Domain.Core.Entities;
using Anima.Projeto.Domain.Shared.Interfaces;

namespace Anima.Projeto.Application.Queries
{
    public class GetMediaByIdQuery : Query<GetMediaByIdRequest, GetMediaByIdResponse>
    {
        public GetMediaByIdQuery(IReadRepository repository) : base(repository)
        {
        }
        public override GetMediaByIdResponse Handle(GetMediaByIdRequest request)
        {

            Media media = _repository.AsQueryableString<Media>("Usuario", "Usuario.Notas", "Usuario.Respostas").FirstOrDefault(x => x.Id == request.Id);

            var response = new GetMediaByIdResponse();

            if (media == null)
            {
                response.AddError("Media não encontrado");
                return response;
            }

            return new GetMediaByIdResponse
            {
                Id = media.Id,
                CreatedAt = media.CreatedAt,
                Total = media.Total,
                IsActive = media.IsActive,
                Estudante = media.Usuario,
                UpdatedAt = media.UpdatedAt
            };

        }
    }
}
