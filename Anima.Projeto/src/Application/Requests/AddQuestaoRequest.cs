using System;
using Anima.Projeto.Application.Common;

namespace Anima.Projeto.Application.Requests
{
    public class AddQuestaoRequest : Request
    {
        public string Enunciado { get; set; }
        public Guid AvaliacaoId { get; set; }
    }
}
