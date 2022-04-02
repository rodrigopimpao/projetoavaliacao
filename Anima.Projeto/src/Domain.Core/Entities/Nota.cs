using System;
using System.Text.Json.Serialization;
using Anima.Projeto.Domain.Shared.Entities;

namespace Anima.Projeto.Domain.Core.Entities
{
    public class Nota : Entity
    {
        private Nota() { }

        public Nota(double valor, Guid usuarioId, Guid avaliacaoId) : base(Guid.NewGuid())
        {
            Valor = valor;
            UsuarioId = usuarioId;
            AvaliacaoId = avaliacaoId;
        }

        public double Valor { get; set; }

        [JsonIgnore]
        public Guid UsuarioId { get; set; }
        [JsonIgnore]
        public Usuario Usuario { get; set; }
        [JsonIgnore]
        public Guid AvaliacaoId { get; set; }
        [JsonIgnore]
        public Avaliacao Avaliacao { get; set; }

    }
}
