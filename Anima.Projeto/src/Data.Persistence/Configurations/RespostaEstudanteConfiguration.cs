using System;
using Anima.Projeto.Domain.Core.Entities;
using Anima.Projeto.Infrastructure.Data.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anima.Projeto.Infrastructure.Data.Persistence.Configurations
{
    public class RespostaEstudanteConfiguration : EntityConfiguration<RespostaEstudante>
    {
        public override void Configure(EntityTypeBuilder<RespostaEstudante> builder)
        {

            base.Configure(builder);

            builder.ToTable("TB_RESPOSTA_USUARIO");

            builder
                .Property(x => x.UsuarioId)
                .IsRequired()
                .HasColumnName("USUARIO_ID");

            builder
                .Property(x => x.QuestaoId)
                .IsRequired(false)
                .HasColumnName("QUESTAO_ID");

            builder
                .Property(x => x.AlternativaId)
                .IsRequired()
                .HasColumnName("ALTERNATIVA_ID");

            builder
                .HasIndex(p => new { p.UsuarioId, p.QuestaoId })
                .IsUnique(true);

            builder
                .HasOne<Usuario>(s => s.Usuario)
                .WithMany(g => g.Respostas)
                .HasForeignKey(s => s.UsuarioId);

            builder
                .HasOne<Questao>(sc => sc.Questao)
                .WithMany(s => s.Respostas)
                .HasForeignKey(sc => sc.QuestaoId);

            builder
                .HasOne<Alternativa>(sc => sc.Alternativa)
                .WithMany(s => s.Respostas)
                .HasForeignKey(sc => sc.AlternativaId);

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
