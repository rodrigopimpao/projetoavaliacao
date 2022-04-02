using System;
namespace Anima.Projeto.Domain.Shared.Entities
{
    public abstract class Entity
    {
        //para o EF saber instanciar qualquer entidade deve-se usar o construtor em branco
        protected Entity(){
        }

        //para enriquecer o negocio e agilizar o desenvolvimento, usamos um construtor customizado
        public Entity(Guid id)
        {
            Id = id;
            var now = DateTime.Now;
            UpdatedAt = now;
            CreatedAt = now;
            IsActive = true;
        }
        
        public Guid Id { get; set; }        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
