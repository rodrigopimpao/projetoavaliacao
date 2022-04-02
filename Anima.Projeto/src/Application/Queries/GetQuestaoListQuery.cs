using System.Collections.Generic;
using System.Linq;
using Anima.Projeto.Application.Common;
using Anima.Projeto.Application.Requests;
using Anima.Projeto.Domain.Core.Entities;
using Anima.Projeto.Domain.Shared.Interfaces;

namespace Anima.Projeto.Application.Queries
{
    public class GetQuestaoListQuery : Query<GetQuestaoByIdRequest, GetQuestaoListResponse>
    {
        public GetQuestaoListQuery(IReadRepository repository) : base(repository)
        {
        }
        public override GetQuestaoListResponse Handle(GetQuestaoByIdRequest request)
        {
            List<Questao> questao = _repository.AsQueryable<Questao>().ToList();

            var response = new GetQuestaoListResponse();

            if (!questao.Any())
            {
                response.AddError("Nenhuma questão cadastrada");
                return response;
            }

            return new GetQuestaoListResponse
            {
                Questaos = questao
            };

        }
    }
}
