using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estoque.Produto.Api.Data;

public class ProdutoMapping : IEntityTypeConfiguration<Domain.Produto>
{
    public void Configure(EntityTypeBuilder<Domain.Produto> builder)
    {
        builder.ToTable("produto");

        builder.HasKey(p => p.Id).HasName("id");
        builder.Property(p => p.Nome).HasColumnName("nome").IsRequired();
        builder.Property(p => p.UnidadeMedida).HasColumnName("unidade_medida").IsRequired();
        builder.Property(p => p.PrecoVenda).HasColumnName("preco_venda").HasPrecision(15, 2).IsRequired();
    }
}