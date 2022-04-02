using System.Linq;
using Anima.Projeto.Application.Common;
using Anima.Projeto.Application.Requests;
using Anima.Projeto.Domain.Core.Entities;
using Anima.Projeto.Domain.Shared.Interfaces;

namespace Anima.Projeto.Application.Queries
{
    public class GetQuestaoByIdQuery : Query<GetQuestaoByIdRequest, GetQuestaoByIdResponse>
    {
        public GetQuestaoByIdQuery(IReadRepository repository) : base(repository)
        {
        }
        public override GetQuestaoByIdResponse Handle(GetQuestaoByIdRequest request)
        {

            Questao questao = _repository.AsQueryableString<Questao>().SingleOrDefault(x => x.Id == request.Id);
            
            var response = new GetQuestaoByIdResponse();

            if (questao == null)
            {
                response.AddError("Avaliação não encontrado");
                return response;
            }

            return new GetQuestaoByIdResponse
            {
                Id = questao.Id,
                CreatedAt = questao.CreatedAt,
                Enunciado = questao.Enunciado,
                Avaliacao = questao.Avaliacao,
                IsActive = questao.IsActive,
                Alternativas = questao.Alternativas,
                UpdatedAt = questao.UpdatedAt
            };

        }
    }
}
