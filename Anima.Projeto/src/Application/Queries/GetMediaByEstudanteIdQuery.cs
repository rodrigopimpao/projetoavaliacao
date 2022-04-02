using System;
using System.Linq;
using Anima.Projeto.Application.Common;
using Anima.Projeto.Application.Requests;
using Anima.Projeto.Domain.Core.Entities;
using Anima.Projeto.Domain.Shared.Interfaces;

namespace Anima.Projeto.Application.Queries
{
    public class GetMediaByEstudanteIdQuery : Query<GetMediaByIdRequest, GetMediaByIdResponse>
    {
        public GetMediaByEstudanteIdQuery(IReadRepository repository) : base(repository)
        {
        }
        public override GetMediaByIdResponse Handle(GetMediaByIdRequest request)
        {


            //var Questaos = _repository.AsQueryable<Questao>(x => x.Avaliacao, y => y.Alternativas).Where(x => x.Avaliacao.Id == request.Id).Where(y => y.Alternativas.Any() == true);


            Media media = _repository.AsQueryable<Media>(x => x.Usuario).FirstOrDefault(x => x.UsuarioId == request.EstudanteId);


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
