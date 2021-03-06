using System;
using Anima.Projeto.Application.Common;

namespace Anima.Projeto.Application.Requests
{
    public class UpdateAlternativaRequest : Request
    {
        private Guid Id { get; set; }
        public string Descricao { get; set; }
        public bool Correta { get; set; }

        public Guid getId()
        {
            return Id;
        }

        public void setId(Guid id)
        {
            Id = id;
        }

    }
}
