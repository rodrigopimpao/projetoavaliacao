using System;
using Anima.Projeto.Application.Common;

namespace Anima.Projeto.Application.Requests
{
    public class AddNotaRequest : Request
    {
        public double Valor { get; set; }

        public Guid EstudanteId { get; set; }

        public Guid AvaliacaoId { get; set; }

    }
}
