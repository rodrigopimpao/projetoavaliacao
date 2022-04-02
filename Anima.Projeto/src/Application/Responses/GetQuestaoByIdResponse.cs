using System;
using System.Collections.Generic;
using Anima.Projeto.Application.Common;
using Anima.Projeto.Domain.Core.Entities;

namespace Anima.Projeto.Application.Requests
{
    public class GetQuestaoByIdResponse : Response
    {
        public Guid Id { get; set; }
        public Avaliacao Avaliacao { get; set; }
        public string Enunciado { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Alternativa> Alternativas { get; set; }
    }
}
