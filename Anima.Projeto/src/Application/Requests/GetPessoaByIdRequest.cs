using System;
using Anima.Projeto.Application.Common;

namespace Anima.Projeto.Application.Requests
{
    public class GetPessoaByIdRequest : Request
    {
        public Guid Id { get; set; }
    }
}
