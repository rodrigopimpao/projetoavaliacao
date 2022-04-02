using System.Linq;
using Anima.Projeto.Application.Common;
using Anima.Projeto.Application.Requests;
using Anima.Projeto.Domain.Core.Entities;
using Anima.Projeto.Domain.Shared.Interfaces;

namespace Anima.Projeto.Application.Queries
{
    public class GetNotaByEstudanteIdQuery : Query<GetNotaByIdRequest, GetNotaByIdResponse>
    {
        public GetNotaByEstudanteIdQuery(IReadRepository repository) : base(repository)
        {
        }
        public override GetNotaByIdResponse Handle(GetNotaByIdRequest request)
        {

            Nota nota = _repository.AsQueryableString<Nota>("Usuario", "Avaliacao").FirstOrDefault(x => x.UsuarioId == request.EstudanteId);
            
            var response = new GetNotaByIdResponse();

            if (nota == null)
            {
                response.AddError("Nota não encontrado");
                return response;
            }



            return new GetNotaByIdResponse
            {
                Id = nota.Id,
                CreatedAt = nota.CreatedAt,
                Valor = nota.Valor,
                IsActive = nota.IsActive,
                Estudante = nota.Usuario,
                UpdatedAt = nota.UpdatedAt,
                Avaliacao = nota.Avaliacao
            };

        }
    }
}
