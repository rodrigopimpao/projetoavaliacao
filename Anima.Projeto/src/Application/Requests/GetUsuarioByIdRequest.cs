using System;
using Anima.Projeto.Application.Common;

namespace Anima.Projeto.Application.Requests
{
    public class GetUsuarioByIdRequest : Request
    {
        public Guid Id { get; set; }

    }
}
