using System;
using Anima.Projeto.Application.Common;

namespace Anima.Projeto.Application.Requests
{
    public class PessoaResponse : Response
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string Funcao { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
