using System;
using Anima.Projeto.Application.Common;

namespace Anima.Projeto.Application.Requests
{
    public class UpdateRespostaEstudanteRequest : Request
    {
        private Guid UsuarioId { get; set; }
        private Guid QuestaoId { get; set; }
        public Guid AlternativaId { get; set; }

        public Guid getUsuarioId()
        {
            return UsuarioId;
        }
        public void setUsuarioId(Guid usuarioId)
        {
            UsuarioId = usuarioId;
        }

        public Guid getQuestaoId()
        {
            return QuestaoId;
        }
        public void setQuestaoId(Guid questaoId)
        {
            QuestaoId = questaoId;
        }
    }
}
