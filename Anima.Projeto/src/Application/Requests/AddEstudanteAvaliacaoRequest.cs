using System;
using Anima.Projeto.Application.Common;

namespace Anima.Projeto.Application.Requests
{
    public class AddEstudanteAvaliacaoRequest : Request
    {
        public Guid EstudanteId { get; set; }
        public Guid AvaliacaoId { get; set; }
        public string Token { get; set; }
    }
}
