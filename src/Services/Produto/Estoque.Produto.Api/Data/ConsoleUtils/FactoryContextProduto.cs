using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
namespace Estoque.Produto.Api.Data.ConsoleUtils;

public class FactoryContextProduto : IDesignTimeDbContextFactory<ProdutoContext>
{
    public ProdutoContext CreateDbContext(string[] args)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        var configuration = builder.Build();


        var optionsBuilder = new DbContextOptionsBuilder<ProdutoContext>();

        optionsBuilder.UseNpgsql(configuration.GetConnectionString("Dados"));

        return new ProdutoContext(optionsBuilder.Options, null);
    }
}