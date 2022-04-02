using System;
using System.Linq;
using Anima.Projeto.Application.Common;
using Anima.Projeto.Application.Requests;
using Anima.Projeto.Domain.Core.Entities;
using Anima.Projeto.Domain.Shared.Interfaces;

namespace Anima.Projeto.Application.Queries
{
    public class GetProfessorByIdQuery : Query<GetUsuarioByIdRequest, GetUsuarioByIdResponse>
    {
        public GetProfessorByIdQuery(IReadRepository repository) : base(repository)
        {
        }
        public override GetUsuarioByIdResponse Handle(GetUsuarioByIdRequest request)
        {

            Usuario usuario = _repository.AsQueryable<Usuario>().SingleOrDefault(x => x.Id == request.Id && x.Funcao == "Professor");

            var response = new GetUsuarioByIdResponse();

            if (usuario == null)
            {
                response.AddError("Professor não encontrado");
                return response;
            }

            return new GetUsuarioByIdResponse
            {
                Id = usuario.Id,
                Login = usuario.Login,
                CreatedAt = usuario.CreatedAt,
                Email = usuario.Email,
                IsActive = usuario.IsActive,
                Nome = usuario.Nome,
                UpdatedAt = usuario.UpdatedAt                
            };
            
        }
    }
}
