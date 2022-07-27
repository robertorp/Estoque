using System.IO;
using System.Reflection;
using System.Windows;
using AppWpf.Produtos;
using Estoque.Core.Communication.Mediator;
using Estoque.Produto.Api.Application;
using Estoque.Produto.Api.Data;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppWpf
{
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            var serviceCollection = new ServiceCollection();

            serviceCollection.AddTransient<RepositorioPessoa>();

            serviceCollection.AddValidatorsFromAssembly(Assembly.Load("Estoque.Produto.Api"));
            serviceCollection.AddMediatR(config =>
            {
                config.AsScoped();
            }, Assembly.Load("Estoque.Produto.Api"));

            serviceCollection.AddScoped<IMediatorHandler, MediatorHandler>();

            serviceCollection.AddDbContext<ProdutoContext>(o =>
                o.UseNpgsql(Configuration.GetConnectionString("Dados"))
                    .UseSnakeCaseNamingConvention());


            serviceCollection.AddScoped<IProdutoAppService, ProdutoAppService>();

            serviceCollection.AddSingleton<MainWindow>();
            serviceCollection.AddTransient<NovoProdutoContent>();
            serviceCollection.AddTransient<NovoProdutoContext>();
            serviceCollection.AddTransient<ListarProdutosContent>();
            serviceCollection.AddTransient<ListarProdutosContext>();


            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWidow = ServiceProvider.GetService<MainWindow>();
            mainWidow.Show();
        }
    }
}
