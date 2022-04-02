using System.Linq;
using Anima.Projeto.Application.Common;
using Anima.Projeto.Application.Requests;
using Anima.Projeto.Domain.Core.Entities;
using Anima.Projeto.Domain.Shared.Interfaces;

namespace Anima.Projeto.Application.Queries
{
    public class GetNotaByIdQuery : Query<GetNotaByIdRequest, GetNotaByIdResponse>
    {
        public GetNotaByIdQuery(IReadRepository repository) : base(repository)
        {
        }
        public override GetNotaByIdResponse Handle(GetNotaByIdRequest request)
        {


            IQueryable<Nota> resultado = _repository.AsQueryable<Nota>().Where(x => x.Id == request.Id);

            Nota nota = resultado.FirstOrDefault();

            IQueryable<Usuario> retornoEstudante = _repository.AsQueryable<Usuario>().Where(x => x.Id == nota.UsuarioId);

            Usuario estudate = retornoEstudante.FirstOrDefault();

            nota.Usuario = estudate;

            IQueryable<Avaliacao> retornoAvaliacao = _repository.AsQueryable<Avaliacao>().Where(x => x.Id == nota.AvaliacaoId);

            Avaliacao avaliacao = retornoAvaliacao.FirstOrDefault();

            nota.Avaliacao = avaliacao;

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
