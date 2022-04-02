using System;
using System.Text.Json.Serialization;

namespace Anima.Projeto.Domain.Core.Entities
{
    public class UsuarioAvaliacao
    {
        [JsonIgnore]
        public Guid UsuarioId { get; set; }
        [JsonIgnore]
        public Usuario Usuario { get; set; }

        public Guid AvaliacaoId { get; set; }
        public Avaliacao Avaliacao { get; set; }

    }
}
