using System;
using Anima.Projeto.Application.Common;

namespace Anima.Projeto.Application.Requests
{
    public class UpdateNotaRequest : Request
    {
        private Guid Id { get; set; }
        public double Valor { get; set; }

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
