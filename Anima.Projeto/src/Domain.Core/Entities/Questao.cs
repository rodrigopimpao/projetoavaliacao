using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Anima.Projeto.Domain.Shared.Entities;

namespace Anima.Projeto.Domain.Core.Entities
{
    public class Questao : Entity
    {
        private Questao() { }

        public Questao(string enunciado, Guid avaliacaoId) : base(Guid.NewGuid())
        {
            Enunciado = enunciado;
            AvaliacaoId = avaliacaoId;
        }

        public string Enunciado { get; set; }
        
        public Guid AvaliacaoId { get; set; }
        [JsonIgnore]
        public Avaliacao Avaliacao { get; set; }
        [JsonIgnore]
        public ICollection<RespostaEstudante> Respostas { get; set; }
        public ICollection<Alternativa> Alternativas { get; set; }

    }
}
