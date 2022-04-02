using System;
using Anima.Projeto.Application.Common;

namespace Anima.Projeto.Application.Responses
{
    public class AddNotaResponse : Response
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
