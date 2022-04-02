using System;
using Anima.Projeto.Application.Common;

namespace Anima.Projeto.Application.Requests
{
    public class AddMediaRequest : Request
    {
        public double Total { get; set; }

        public Guid UsuarioId { get; set; }

        public string Token { get; set; }

    }
}
