using Estoque.Core.Data;
using Estoque.Core.Messages;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Estoque.Produto.Api.Data;

public class ProdutoContext : DbContext, IUnitOfWork
{
    private readonly IMediator _mediator;

    public ProdutoContext(
        DbContextOptions<ProdutoContext> options, 
        IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }

    public DbSet<Domain.Produto> Produtos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ActiveLogOnDebug();
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Ignore<ValidationResult>();
        modelBuilder.Ignore<Event>();

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ProdutoContext).Assembly,
            type => type.Namespace == "Estoque.Produto.Api.Data");

        modelBuilder.StringVarchar();
        modelBuilder.ToSnakeNames();
    }

    public async Task<bool> SaveEntitiesAsync()
    {
        return await this.SaveEntitiesWithPublishEvents(_mediator);
    }
}