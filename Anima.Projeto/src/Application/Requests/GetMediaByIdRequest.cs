using System;
using Anima.Projeto.Application.Common;

namespace Anima.Projeto.Application.Requests
{
    public class GetMediaByIdRequest : Request
    {
        public Guid Id { get; set; }
        public Guid EstudanteId { get; set; }
    }
}
