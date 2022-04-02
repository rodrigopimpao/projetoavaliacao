using System;
using Anima.Projeto.Application.Common;
using Anima.Projeto.Domain.Core.Entities;

namespace Anima.Projeto.Application.Requests
{
    public class GetRespostaEstudanteByIdResponse : Response
    {
        public Guid Id { get; set; }
        //public Usuario Usuario { get; set; }
        public Questao Questao { get; set; }
        public Alternativa Resposta { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
