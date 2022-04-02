using System.Collections.Generic;
using System.Linq;
using Anima.Projeto.Application.Common;
using Anima.Projeto.Application.Requests;
using Anima.Projeto.Domain.Core.Entities;
using Anima.Projeto.Domain.Shared.Interfaces;

namespace Anima.Projeto.Application.Queries
{
    public class GetUsuarioListQuery : Query<GetUsuarioByIdRequest, GetUsuarioListResponse>
    {
        public GetUsuarioListQuery(IReadRepository repository) : base(repository)
        {
        }
        public override GetUsuarioListResponse Handle(GetUsuarioByIdRequest request)
        {
            List<Usuario> usuario = _repository.AsQueryable<Usuario>().ToList();

            var response = new GetUsuarioListResponse();

            if (usuario == null)
            {
                response.AddError("Usuário não encontrado");
                return response;
            }

            return new GetUsuarioListResponse
            {
                Usuarios = usuario
            };

        }
    }
}
