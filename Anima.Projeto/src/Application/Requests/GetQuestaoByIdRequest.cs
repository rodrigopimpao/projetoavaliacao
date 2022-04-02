using System;
using Anima.Projeto.Application.Common;

namespace Anima.Projeto.Application.Requests
{
    public class GetQuestaoByIdRequest : Request
    {
        public Guid Id { get; set; }
    }
}
