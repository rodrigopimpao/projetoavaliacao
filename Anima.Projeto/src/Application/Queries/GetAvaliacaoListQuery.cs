using System.Collections.Generic;
using System.Linq;
using Anima.Projeto.Application.Common;
using Anima.Projeto.Application.Requests;
using Anima.Projeto.Domain.Core.Entities;
using Anima.Projeto.Domain.Shared.Interfaces;

namespace Anima.Projeto.Application.Queries
{
    public class GetAvaliacaoListQuery : Query<GetAvaliacaoByIdRequest, GetAvaliacaoListResponse>
    {
        public GetAvaliacaoListQuery(IReadRepository repository) : base(repository)
        {
        }
        public override GetAvaliacaoListResponse Handle(GetAvaliacaoByIdRequest request)
        {
            List<Avaliacao> avaliacao = _repository.AsQueryableString<Avaliacao>("Notas").OrderBy(x => x.Nome).ToList();

            var response = new GetAvaliacaoListResponse();

            //if (!avaliacao.Any())
            //{
            //    response.AddError("Nenhuma avaliação cadastrada");
            //    return response;
            //}

            return new GetAvaliacaoListResponse
            {
                Avaliacaos = avaliacao
            };

        }
    }
}
