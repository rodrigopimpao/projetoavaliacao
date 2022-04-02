using System;
using System.Collections.Generic;
using Anima.Projeto.Application.Common;
using Anima.Projeto.Domain.Core.Entities;

namespace Anima.Projeto.Application.Requests
{
    public class GetAlternativaByIdResponse : Response
    {
        public Guid Id { get; set; }
        public Questao Questao { get; set; }
        public string Descricao { get; set; }
        public bool Correta { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public ICollection<RespostaEstudante> Respostas { get; set; }

    }
}
