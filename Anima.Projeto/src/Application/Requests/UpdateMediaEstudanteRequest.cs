using System;
using Anima.Projeto.Application.Common;

namespace Anima.Projeto.Application.Requests
{
    public class UpdateMediaEstudanteRequest : Request
    {
        private Guid UsuarioId { get; set; }
        public double Total { get; set; }

        public Guid getUsuarioId()
        {
            return UsuarioId;
        }

        public void setUsuarioId(Guid usuarioId)
        {
            UsuarioId = usuarioId;
        }

    }
}
