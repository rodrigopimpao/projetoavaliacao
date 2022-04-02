using System;
using Anima.Projeto.Application.Common;

namespace Anima.Projeto.Application.Requests
{
    public class AddRespostaEstudanteRequest : Request
    {
        public Guid UsuarioId { get; set; }

        public Guid QuestaoId { get; set; }

        public Guid AlternativaId { get; set; }

    }
}
