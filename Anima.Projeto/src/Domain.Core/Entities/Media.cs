using System;
using System.Text.Json.Serialization;
using Anima.Projeto.Domain.Shared.Entities;

namespace Anima.Projeto.Domain.Core.Entities
{
    public class Media : Entity
    {
        private Media() { }

        public Media(double total, Guid usuarioId) : base(Guid.NewGuid())
        {
            Total = total;
            UsuarioId = usuarioId;
        }

        public double Total { get; set; }

        public Guid UsuarioId { get; set; }
        [JsonIgnore]
        public Usuario Usuario { get; set; }

    }
}
