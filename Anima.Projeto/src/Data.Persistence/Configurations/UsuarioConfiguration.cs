using System;
using Anima.Projeto.Domain.Core.Entities;
using Anima.Projeto.Infrastructure.Data.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anima.Projeto.Infrastructure.Data.Persistence.Configurations
{
    public class UsuarioConfiguration : EntityConfiguration<Usuario>
    {
        public override void Configure(EntityTypeBuilder<Usuario> builder)
        {
            base.Configure(builder);

            builder.ToTable("TB_USUARIO");

            builder
                .Property(x => x.Nome)
                .IsRequired()
                .HasColumnName("NOME");

            builder
                .Property(x => x.Email)
                .IsRequired()
                .HasColumnName("EMAIL");

            builder
               .Property(x => x.CPF)
               .IsRequired()
               .HasColumnName("CPF");

            builder
                .Property(x => x.Login)
                .IsRequired()
                .HasColumnName("LOGIN");

            builder
                .Property(x => x.Senha)
                .IsRequired()
                .HasColumnName("SENHA");

            builder
                .Property(x => x.Funcao)
                .IsRequired()
                .HasColumnName("FUNCAO");

        }

        //exemplo para customizar entidades com chave composta
        //public override void ConfigureKey(EntityTypeBuilder<Customer> builder)
        //{
        //    builder.HasKey(x => x.Id);
        //    builder.HasKey(x => x.Name);
        //}

    }
}

/*

sugestao de padrao para nomes de colunas
ID  => identificadores
ID_ => coluna de chave estrangeira
NM_ -> para nomes
VL_ -> valores, grana, dinheiros, din dins,
NR_ -> numeros, numero de endereço, taxas, percuntais
DT_ -> datas
DS_ -> descrição
ST_ -> status, binario, enumeradores

sugestao para nomenclatura de estruturas de bases de dados relacionais
TB_ -> tabelas
IX_ -> indices
PK_ -> chave primaria
FK_ -> chave estrangeira
 
*/
