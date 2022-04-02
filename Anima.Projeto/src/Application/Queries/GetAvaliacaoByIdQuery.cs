using System;
using System.Collections.Generic;
using System.Linq;
using Anima.Projeto.Application.Common;
using Anima.Projeto.Application.Requests;
using Anima.Projeto.Domain.Core.Entities;
using Anima.Projeto.Domain.Shared.Interfaces;

namespace Anima.Projeto.Application.Queries
{
    public class GetAvaliacaoByIdQuery : Query<GetAvaliacaoByIdRequest, GetAvaliacaoByIdResponse>
    {
        public GetAvaliacaoByIdQuery(IReadRepository repository) : base(repository)
        {
        }
        public override GetAvaliacaoByIdResponse Handle(GetAvaliacaoByIdRequest request)
        {



            Avaliacao avaliacao = _repository.AsQueryableString<Avaliacao>("Questaos", "Questaos.Alternativas", "Notas").SingleOrDefault(x => x.Id == request.Id);
            //var Questaos = _repository.AsQueryable<Questao>(x => x.Avaliacao, y => y.Alternativas).Where(x => x.Avaliacao.Id == request.Id).Where(y => y.Alternativas.Any() == true);

            var response = new GetAvaliacaoByIdResponse();

            if (avaliacao == null)
            {
                response.AddError("Avaliação não encontrado");
                return response;
            }

            return new GetAvaliacaoByIdResponse
            {
                Id = avaliacao.Id,
                CreatedAt = avaliacao.CreatedAt,
                Nome = avaliacao.Nome,
                Descricao = avaliacao.Descricao,
                IsActive = avaliacao.IsActive,
                Questaos = avaliacao.Questaos,
                Notas = avaliacao.Notas,
                UpdatedAt = avaliacao.UpdatedAt
            };

        }
    }
}
