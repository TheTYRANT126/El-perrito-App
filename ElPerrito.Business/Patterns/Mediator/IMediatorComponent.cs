namespace ElPerrito.Business.Patterns.Mediator
{
    /// <summary>
    /// Patrón Mediator - Componente que se comunica a través del mediador
    /// </summary>
    public interface IMediatorComponent
    {
        void SetMediator(IMediator mediator);
        string GetComponentName();
    }
}
