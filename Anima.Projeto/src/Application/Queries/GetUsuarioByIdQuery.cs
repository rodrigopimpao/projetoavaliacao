using System;
using System.Collections.Generic;
using System.Linq;
using Anima.Projeto.Application.Common;
using Anima.Projeto.Application.Requests;
using Anima.Projeto.Domain.Core.Entities;
using Anima.Projeto.Domain.Shared.Interfaces;

namespace Anima.Projeto.Application.Queries
{
    public class GetUsuarioByIdQuery : Query<GetUsuarioByIdRequest, GetUsuarioByIdResponse>
    {
        public GetUsuarioByIdQuery(IReadRepository repository) : base(repository)
        {
        }
        public override GetUsuarioByIdResponse Handle(GetUsuarioByIdRequest request)
        {
            Usuario usuario = _repository.AsQueryable<Usuario>().FirstOrDefault(x => x.Id == request.Id);

            var response = new GetUsuarioByIdResponse();

            if (usuario == null)
            {
                response.AddError("Usuário não encontrado");
                return response;
            }

            return new GetUsuarioByIdResponse
            {
                Id = usuario.Id,
                CreatedAt = usuario.CreatedAt,
                IsActive = usuario.IsActive,
                Login = usuario.Login,
                UpdatedAt = usuario.UpdatedAt
            };

       

        }
    }
}
