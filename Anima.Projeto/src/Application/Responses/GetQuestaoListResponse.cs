using System;
using System.Collections.Generic;
using Anima.Projeto.Application.Common;
using Anima.Projeto.Domain.Core.Entities;

namespace Anima.Projeto.Application.Requests
{
    public class GetQuestaoListResponse : Response
    {
       public ICollection<Questao> Questaos { get; set; }
    }
}
