using System;
using Anima.Projeto.Application.Common;

namespace Anima.Projeto.Application.Requests
{
    public class GetUsuarioByLoginRequest : Request
    {
        public string Login { get; set; }

        public string Senha { get; set; }

    }
}
