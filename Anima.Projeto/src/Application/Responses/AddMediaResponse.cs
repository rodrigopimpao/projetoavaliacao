using System;
using Anima.Projeto.Application.Common;

namespace Anima.Projeto.Application.Responses
{
    public class AddMediaResponse : Response
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
