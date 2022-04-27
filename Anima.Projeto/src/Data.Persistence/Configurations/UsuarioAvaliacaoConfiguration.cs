using System;
using Anima.Projeto.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anima.Projeto.Infrastructure.Data.Persistence.Configurations
{
    public class UsuarioAvaliacaoConfiguration : IEntityTypeConfiguration<UsuarioAvaliacao>
    {
        public void Configure(EntityTypeBuilder<UsuarioAvaliacao> builder)
        {

            builder.ToTable("TB_USUARIO_AVALIACAO");

            builder
                .Property(x => x.UsuarioId)
                .IsRequired()
                .HasColumnName("USUARIO_ID");

            builder
                .Property(x => x.AvaliacaoId)
                .IsRequired()
                .HasColumnName("AVALIACAO_ID");

            builder
                .HasKey(sc => new { sc.UsuarioId, sc.AvaliacaoId });

            builder
                .HasOne<Usuario>(sc => sc.Usuario)
                .WithMany(s => s.UsuarioAvaliacaos)
                .HasForeignKey(sc => sc.UsuarioId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder
                .HasOne<Avaliacao>(sc => sc.Avaliacao)
                .WithMany(s => s.UsuarioAvaliacaos)
                .HasForeignKey(sc => sc.AvaliacaoId)
                .OnDelete(DeleteBehavior.ClientCascade);

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
