namespace ElPerrito.Business.Patterns.Observer
{
    /// <summary>
    /// Patrón Observer - Sujeto
    /// </summary>
    public interface ISubject<T>
    {
        void Attach(IObserver<T> observer);
        void Detach(IObserver<T> observer);
        void Notify(T data);
    }
}
