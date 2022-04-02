using System;
using Anima.Projeto.Application.Common;

namespace Anima.Projeto.Application.Requests
{
    public class AddPessoaRequest : Request
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
    }
}
