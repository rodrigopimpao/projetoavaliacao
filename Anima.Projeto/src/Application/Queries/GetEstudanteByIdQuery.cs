using System;
using System.Collections.Generic;
using System.Linq;
using Anima.Projeto.Application.Commands;
using Anima.Projeto.Application.Common;
using Anima.Projeto.Application.Requests;
using Anima.Projeto.Domain.Core.Entities;
using Anima.Projeto.Domain.Shared.Interfaces;

namespace Anima.Projeto.Application.Queries
{
    public class GetEstudanteByIdQuery : Query<GetUsuarioByIdRequest, GetUsuarioByIdResponse>
    {
        public GetEstudanteByIdQuery(IReadRepository repository) : base(repository)
        {
        }
        public override GetUsuarioByIdResponse Handle(GetUsuarioByIdRequest request)
        {
            Usuario estudante = _repository.AsQueryableString<Usuario>("Notas", "Media", "UsuarioAvaliacaos").SingleOrDefault(x => x.Id == request.Id && x.Funcao == "Estudante");

            //if (estudante == null) return null;


            var response = new GetUsuarioByIdResponse();

            if (estudante == null)
            {
                response.AddError("Estudante não encontrado");
                return response;
            }

            return new GetUsuarioByIdResponse
            {
                Id = estudante.Id,
                Login = estudante.Login,
                CreatedAt = estudante.CreatedAt,
                Email = estudante.Email,
                IsActive = estudante.IsActive,
                Nome = estudante.Nome,
                UpdatedAt = estudante.UpdatedAt,
                Notas = estudante.Notas,
                Media = estudante.Media,
                EstudanteAvaliacaos = estudante.UsuarioAvaliacaos
            };

        }
    }
}
