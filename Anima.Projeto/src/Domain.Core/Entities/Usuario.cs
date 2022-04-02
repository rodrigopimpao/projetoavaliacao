using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Anima.Projeto.Domain.Shared.Entities;

namespace Anima.Projeto.Domain.Core.Entities
{
    public class Usuario : Pessoa
    {
        public Usuario() { }

        public Usuario(string login, string senha, string nome, string email, string cpf, string funcao) : base(Guid.NewGuid())
        {
            Login = login;
            Senha = senha;
            Nome = nome;
            Email = email;
            CPF = cpf;
            Funcao = funcao;
        }

        public string Login { get; set; }
        [JsonIgnore]
        public string Senha { get; set; }
        public string Funcao { get; set; }

        public ICollection<UsuarioAvaliacao> UsuarioAvaliacaos { get; set; }

        public ICollection<RespostaEstudante> Respostas { get; set; }

        public ICollection<Nota> Notas { get; set; }
        [JsonIgnore]
        public Media Media { get; set; }

    }
}
