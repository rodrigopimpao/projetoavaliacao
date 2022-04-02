using System;
using Anima.Projeto.Application.Common;
using Anima.Projeto.Domain.Core.Entities;

namespace Anima.Projeto.Application.Requests
{
    public class AddUsuarioRequest : AddPessoaRequest
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Funcao { get; set; }
    }
}
