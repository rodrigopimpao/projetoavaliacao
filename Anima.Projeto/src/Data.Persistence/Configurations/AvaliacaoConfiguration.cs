using System;
using Anima.Projeto.Domain.Core.Entities;
using Anima.Projeto.Infrastructure.Data.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anima.Projeto.Infrastructure.Data.Persistence.Configurations
{
    public class AvaliacaoConfiguration : EntityConfiguration<Avaliacao>
    {
        public override void Configure(EntityTypeBuilder<Avaliacao> builder)
        {
            base.Configure(builder);

            builder.ToTable("TB_AVALIACAO");

            builder
                .Property(x => x.Nome)
                .IsRequired()
                .HasColumnName("NOME");

            builder
                .Property(x => x.Descricao)
                .IsRequired()
                .HasColumnName("DESCRICAO");

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
