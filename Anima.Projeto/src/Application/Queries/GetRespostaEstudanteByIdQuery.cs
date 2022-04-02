using System;
using System.Linq;
using Anima.Projeto.Application.Common;
using Anima.Projeto.Application.Requests;
using Anima.Projeto.Domain.Core.Entities;
using Anima.Projeto.Domain.Shared.Interfaces;

namespace Anima.Projeto.Application.Queries
{
    public class GetRespostaEstudanteByIdQuery : Query<GetRespostaEstudanteByIdRequest, GetRespostaEstudanteByIdResponse>
    {
        public GetRespostaEstudanteByIdQuery(IReadRepository repository) : base(repository)
        {
        }
        public override GetRespostaEstudanteByIdResponse Handle(GetRespostaEstudanteByIdRequest request)
        {


            var respostaEstudante = _repository.AsQueryableString<RespostaEstudante>("Questao", "Alternativa").FirstOrDefault(x => x.UsuarioId == request.EstudanteId && x.QuestaoId == request.QuestaoId);

            var response = new GetRespostaEstudanteByIdResponse();

            if (respostaEstudante == null)
            {
                response.AddError("Resposta da questão não encontrado");
                return response;
            }

            return new GetRespostaEstudanteByIdResponse
            {
                Id = respostaEstudante.Id,
                CreatedAt = respostaEstudante.CreatedAt,
                //Usuario = respostaEstudante.Usuario,
                Questao = respostaEstudante.Questao,
                Resposta = respostaEstudante.Alternativa,
                IsActive = respostaEstudante.IsActive,
                UpdatedAt = respostaEstudante.UpdatedAt
            };

        }
    }
}
