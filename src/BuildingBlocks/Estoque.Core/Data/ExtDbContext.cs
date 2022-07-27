using Estoque.Core.DomainObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Estoque.Core.Data;

public static class ExtDbContext
{
    public static async Task<bool> SaveEntitiesWithPublishEvents(this DbContext dbContext, IMediator mediator)
    {
        // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
        // performed through the DbContext will be committed
        var success = await dbContext.SaveChangesAsync() > 0;

        // Dispatch Domain Events collection. 
        // Choices:
        // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
        // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
        // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
        // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
        if (success)
            await PublishDomainEvents(dbContext, mediator).ConfigureAwait(false);

        return success;
    }

    private static async Task PublishDomainEvents(DbContext dbContext, IMediator mediator)
    {
        var domainEntities = dbContext.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.Notificacoes != null && x.Entity.Notificacoes.Any())
            .ToArray();

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.Notificacoes)
            .ToList();

        domainEntities
            .ToList().ForEach(entity => entity.Entity.ClearEvents());

        var tasks = domainEvents
            .Select(async notification => await mediator.Publish(notification));

        await Task.WhenAll(tasks);
    }

    public static void ActiveLogOnDebug(this DbContextOptionsBuilder opcoes)
    {
#if DEBUG
        opcoes.EnableDetailedErrors();
        opcoes.EnableSensitiveDataLogging();
#endif
    }
}