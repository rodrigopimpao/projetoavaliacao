using Anima.Projeto.Domain.Shared.Entities;
using System;
using System.Text.Json.Serialization;

namespace Anima.Projeto.Domain.Core.Entities
{
    public class RespostaEstudante : Entity
    {

        private RespostaEstudante() { }

        public RespostaEstudante(Guid questaoId, Guid usuarioId, Guid alternativaId) : base(Guid.NewGuid())
        {
            QuestaoId = questaoId;
            UsuarioId = usuarioId;
            AlternativaId = alternativaId;
        }

        [JsonIgnore]
        public Guid UsuarioId { get; set; }
        [JsonIgnore]
        public Usuario Usuario { get; set; }

        public Guid? QuestaoId { get; set; }
        public Questao Questao { get; set; }

        public Guid AlternativaId { get; set; }
        public Alternativa Alternativa { get; set; }

    }
}
