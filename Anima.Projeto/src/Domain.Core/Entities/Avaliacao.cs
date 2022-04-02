using System;
using System.Collections.Generic;
using Anima.Projeto.Domain.Shared.Entities;

namespace Anima.Projeto.Domain.Core.Entities
{
    public class Avaliacao : Entity
    {
        private Avaliacao() { }

        public Avaliacao(string nome, string descricao) : base(Guid.NewGuid())
        {
            Nome = nome;
            Descricao = descricao;
        }

        public string Nome { get; set; }
        public string Descricao { get; set; }

        public IList<UsuarioAvaliacao> UsuarioAvaliacaos { get; set; }

        public ICollection<Questao> Questaos { get; set; }

        public ICollection<Nota> Notas { get; set; }

    }
}
