using System;
using Anima.Projeto.Domain.Shared.Entities;

namespace Anima.Projeto.Domain.Core.Entities
{
    public class Customer : Entity
    {
        private Customer() { }

        public Customer(string name, string email) : base(Guid.NewGuid())
        {
            Name = name;
            Email = email;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        
    }
}
