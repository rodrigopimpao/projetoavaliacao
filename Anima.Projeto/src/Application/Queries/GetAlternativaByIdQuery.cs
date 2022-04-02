using System;
using System.Collections.Generic;
using System.Linq;
using Anima.Projeto.Application.Common;
using Anima.Projeto.Application.Requests;
using Anima.Projeto.Domain.Core.Entities;
using Anima.Projeto.Domain.Shared.Interfaces;

namespace Anima.Projeto.Application.Queries
{
    public class GetAlternativaByIdQuery : Query<GetAlternativaByIdRequest, GetAlternativaByIdResponse>
    {
        public GetAlternativaByIdQuery(IReadRepository repository) : base(repository)
        {
        }
        public override GetAlternativaByIdResponse Handle(GetAlternativaByIdRequest request)
        {



            Alternativa alternativa = _repository.AsQueryableString<Alternativa>("Questao", "Respostas").SingleOrDefault(x => x.Id == request.Id);
            //var Questaos = _repository.AsQueryable<Questao>(x => x.Alternativa, y => y.Alternativas).Where(x => x.Alternativa.Id == request.Id).Where(y => y.Alternativas.Any() == true);

            var response = new GetAlternativaByIdResponse();

            if (alternativa == null)
            {
                response.AddError("Avaliação não encontrado");
                return response;
            }

            return new GetAlternativaByIdResponse
            {
                Id = alternativa.Id,
                CreatedAt = alternativa.CreatedAt,
                Descricao = alternativa.Descricao,
                IsActive = alternativa.IsActive,
                Questao = alternativa.Questao,
                Respostas = alternativa.Respostas,
                Correta = alternativa.Correta,
                UpdatedAt = alternativa.UpdatedAt
            };

        }
    }
}
