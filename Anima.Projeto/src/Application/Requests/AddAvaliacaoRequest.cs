using System;
using Anima.Projeto.Application.Common;

namespace Anima.Projeto.Application.Requests
{
    public class AddAvaliacaoRequest : Request
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Token { get; set; }
    }
}
