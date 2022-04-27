using System;
using System.Linq;
using Anima.Projeto.Application.Common;
using Anima.Projeto.Application.Requests;
using Anima.Projeto.Domain.Core.Entities;
using Anima.Projeto.Domain.Shared.Interfaces;

namespace Anima.Projeto.Application.Queries
{
    public class GetUsuarioByLoginQuery : Query<GetUsuarioByLoginRequest, GetUsuarioByLoginResponse>
    {
        public GetUsuarioByLoginQuery(IReadRepository repository) : base(repository)
        {
        }
        public override GetUsuarioByLoginResponse Handle(GetUsuarioByLoginRequest request)
        {

            Usuario usuario = _repository.AsQueryable<Usuario>().FirstOrDefault(x => x.Login == request.Login && x.Senha == request.Senha);
            
            var response = new GetUsuarioByLoginResponse();

            if (usuario == null)
            {
                response.AddError("Login e/ou Senha inválido(s)!");
                return response;
            }

            var token = Token.CreateToken(usuario);
            usuario.Senha = "";
            
            return new GetUsuarioByLoginResponse
            {
                Id = usuario.Id,
                Token = token,
                Nome = usuario.Nome,
                Email = usuario.Email,
                CPF = usuario.CPF,
                Funcao = usuario.Funcao,
                CreatedAt = usuario.CreatedAt,
                IsActive = usuario.IsActive,
                Login = usuario.Login,
                UpdatedAt = usuario.UpdatedAt
                
            };

        }
    }
}
