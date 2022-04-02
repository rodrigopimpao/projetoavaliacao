using System;
using Anima.Projeto.Application.Common;

namespace Anima.Projeto.Application.Requests
{
    public class UpdateNotaEstudanteAvaliacaoRequest : Request
    {
        private Guid UsuarioId { get; set; }
        private Guid AvaliacaoId { get; set; }
        public double Valor { get; set; }

        public Guid getUsuarioId()
        {
            return UsuarioId;
        }
        public void setUsuarioId(Guid usuarioId)
        {
            UsuarioId = usuarioId;
        }

        public Guid getAvaliacaoId()
        {
            return AvaliacaoId;
        }
        public void setAvaliacaoId(Guid avaliacaoId)
        {
            AvaliacaoId = avaliacaoId;
        }

    }
}
