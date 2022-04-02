using System;
using System.Collections.Generic;
using Anima.Projeto.Application.Common;
using Anima.Projeto.Domain.Core.Entities;

namespace Anima.Projeto.Application.Requests
{
    public class GetUsuarioByIdResponse : PessoaResponse
    {
        public string Login { get; set; }
        public Media Media { get; set; }
        public ICollection<Nota> Notas { get; set; }
        public ICollection<UsuarioAvaliacao> EstudanteAvaliacaos { get; set; }
    }
}
