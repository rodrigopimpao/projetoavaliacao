using Anima.Projeto.Domain.Core.Entities;
using System;
namespace Anima.Projeto.Domain.Shared.Entities
{
    public abstract class Pessoa : Entity
    {
        //para o EF saber instanciar qualquer entidade deve-se usar o construtor em branco
        protected Pessoa() {
        }

        public Pessoa(Guid id)
        {
            Id = id;
            var now = DateTime.Now;
            UpdatedAt = now;
            CreatedAt = now;
            IsActive = true;
        }
       
        public Pessoa(string nome, string email, string cpf) : base(Guid.NewGuid())
        {
            Nome = nome;
            Email = email;
            CPF = cpf;
        }

        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }

    }
}
