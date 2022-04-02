using System;
using Anima.Projeto.Application.Common;

namespace Anima.Projeto.Application.Requests
{
    public class GetAvaliacaoByIdRequest : Request
    {
        public Guid Id { get; set; }
    }
}
