using System;
using Anima.Projeto.Application.Common;

namespace Anima.Projeto.Application.Responses
{
    public class UpdateAvaliacaoResponse : Response
    {
        public Guid Id { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
