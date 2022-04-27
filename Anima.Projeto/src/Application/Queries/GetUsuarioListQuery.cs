using System;
using System.Collections.Generic;
using System.Linq;
using Anima.Projeto.Application.Common;
using Anima.Projeto.Application.Requests;
using Anima.Projeto.Domain.Core.Entities;
using Anima.Projeto.Domain.Shared.Interfaces;

namespace Anima.Projeto.Application.Queries
{
    public class GetUsuarioListQuery : Query<GetUsuarioListRequest, GetUsuarioListResponse>
    {
        public GetUsuarioListQuery(IReadRepository repository) : base(repository)
        {
        }
        public override GetUsuarioListResponse Handle(GetUsuarioListRequest request)
        {
            List<Usuario> usuario = null;
            if (request != null && request.Funcao != null)
            {
                usuario = _repository.AsQueryableString<Usuario>("Notas", "Media").Where(x => x.Funcao == request.Funcao).OrderBy(x => x.Nome).ToList();
            } else
            {
                usuario = _repository.AsQueryable<Usuario>().ToList();
            }
            var response = new GetUsuarioListResponse();

            //if (!usuario.Any())
            //{
            //    response.AddError("Nenhum usuário cadastrado");
            //    return response;
            //}

            return new GetUsuarioListResponse
            {
                Usuarios = usuario
            };

        }
    }
}
