namespace ElPerrito.Business.Patterns.Mediator
{
    public interface IMediator
    {
        void Notify(IMediatorComponent sender, string eventName, object? data = null);
    }
}
