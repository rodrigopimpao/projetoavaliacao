using System;
using Anima.Projeto.Domain.Core.Entities;
using Anima.Projeto.Infrastructure.Data.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anima.Projeto.Infrastructure.Data.Persistence.Configurations
{
    public class QuestaoConfiguration : EntityConfiguration<Questao>
    {
        public override void Configure(EntityTypeBuilder<Questao> builder)
        {
            base.Configure(builder);

            builder.ToTable("TB_QUESTAO");

            builder
                .Property(x => x.Enunciado)
                .IsRequired()
                .HasColumnName("ENUNCIADO");

            builder
                .Property(x => x.AvaliacaoId)
                .IsRequired()
                .HasColumnName("AVALIACAO_ID");

            builder
                .HasOne<Avaliacao>(s => s.Avaliacao)
                .WithMany(g => g.Questaos)
                .HasForeignKey(s => s.AvaliacaoId)
                .OnDelete(DeleteBehavior.Restrict);



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
