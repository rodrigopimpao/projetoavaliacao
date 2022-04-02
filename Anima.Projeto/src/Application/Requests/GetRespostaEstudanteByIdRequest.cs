using System;
using Anima.Projeto.Application.Common;

namespace Anima.Projeto.Application.Requests
{
    public class GetRespostaEstudanteByIdRequest : Request
    {
        public Guid EstudanteId { get; set; }
        public Guid QuestaoId { get; set; }
        public Guid AlternativaId { get; set; }
    }
}
