using Estoque.Core.Messages;
using Estoque.Core.Messages.CommonMessages.Notifications;

namespace Estoque.Core.Communication.Mediator;

public interface IMediatorHandler
{
    public Task<GenericResponse> EnviarComando<T>(T comando) where T : Command;
    public Task PublicarEvento<T>(T evento) where T : Event;
    public Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification;
}