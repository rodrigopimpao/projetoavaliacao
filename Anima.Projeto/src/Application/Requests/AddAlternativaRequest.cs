using System;
using Anima.Projeto.Application.Common;

namespace Anima.Projeto.Application.Requests
{
    public class AddAlternativaRequest : Request
    {
        public string Descricao { get; set; }
        public bool Correta { get; set; }
        public Guid QuestaoId { get; set; }
    }
}
