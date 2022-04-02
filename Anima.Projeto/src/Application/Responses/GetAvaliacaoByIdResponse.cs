using System;
using System.Collections.Generic;
using Anima.Projeto.Application.Common;
using Anima.Projeto.Domain.Core.Entities;

namespace Anima.Projeto.Application.Requests
{
    public class GetAvaliacaoByIdResponse : Response
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public IList<UsuarioAvaliacao> EstudateAvaliacaos { get; set; }
        public ICollection<Questao> Questaos { get; set; }
        public ICollection<Nota> Notas { get; set; }

    }
}
