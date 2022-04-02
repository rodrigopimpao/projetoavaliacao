using System;
using Anima.Projeto.Application.Common;

namespace Anima.Projeto.Application.Requests
{
    public class UpdateMediaRequest : Request
    {
        private Guid Id { get; set; }
        public double Total { get; set; }

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
