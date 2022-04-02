using System;
using System.Collections.Generic;
using Anima.Projeto.Application.Common;
using Anima.Projeto.Application.Requests;
using Anima.Projeto.Application.Responses;
using Anima.Projeto.Domain.Core.Entities;
using Anima.Projeto.Domain.Shared.Interfaces;

namespace Anima.Projeto.Application.Commands
{
    // os comandos são instruçoes que alteram o estado do servidor
    public class AddUsuarioCommand : Command<AddUsuarioRequest, AddUsuarioResponse>
    {
        public AddUsuarioCommand(IWriteRepository repository) : base(repository)
        {
        }

        //todo metodo handle tem que caracterizar um transação
        protected override AddUsuarioResponse Changes(AddUsuarioRequest request)
        {
            var usuario = new Usuario(request.Login, request.Senha, request.Nome, request.Email, request.CPF, request.Funcao);

            _repository.Add(usuario);

            return new AddUsuarioResponse
            {
                Id = usuario.Id,
                CreatedAt = usuario.CreatedAt
            };
        }
    }
}
