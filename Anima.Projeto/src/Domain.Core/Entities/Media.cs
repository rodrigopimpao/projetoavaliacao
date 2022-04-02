using System;
using System.Text.Json.Serialization;
using Anima.Projeto.Domain.Shared.Entities;

namespace Anima.Projeto.Domain.Core.Entities
{
    public class Media : Entity
    {
        private Media() { }

        public Media(double total) : base(Guid.NewGuid())
        {
            Total = total;
        }

        public double Total { get; set; }

        public Guid UsuarioId { get; set; }
        [JsonIgnore]
        public Usuario Usuario { get; set; }

    }
}
