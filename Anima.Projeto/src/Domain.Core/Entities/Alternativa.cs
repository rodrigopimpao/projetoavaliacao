using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Anima.Projeto.Domain.Shared.Entities;

namespace Anima.Projeto.Domain.Core.Entities
{
    public class Alternativa : Entity
    {
        private Alternativa() { }

        public Alternativa(string descricao, bool correta, Guid questaoId) : base(Guid.NewGuid())
        {
            Descricao = descricao;
            Correta = correta;
            QuestaoId = questaoId;
        }

        public string Descricao { get; set; }

        public bool Correta { get; set; }
        [JsonIgnore]
        public Guid QuestaoId { get; set; }
        [JsonIgnore]
        public Questao Questao { get; set; }
        [JsonIgnore]
        public ICollection<RespostaEstudante> Respostas { get; set; }

    }
}
