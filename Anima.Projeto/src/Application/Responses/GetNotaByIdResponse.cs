using System;
using Anima.Projeto.Application.Common;
using Anima.Projeto.Domain.Core.Entities;

namespace Anima.Projeto.Application.Requests
{
    public class GetNotaByIdResponse : Response
    {
        public Guid Id { get; set; }
        public double Valor { get; set; }
        public Usuario Estudante { get; set; }
        public Avaliacao Avaliacao { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
