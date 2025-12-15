namespace ElPerrito.Business.Patterns.Mediator
{
    public interface IMediatorComponent
    {
        void SetMediator(IMediator mediator);
        string GetComponentName();
    }
}
