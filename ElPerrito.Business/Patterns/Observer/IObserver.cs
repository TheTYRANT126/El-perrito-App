namespace ElPerrito.Business.Patterns.Observer
{
    /// <summary>
    /// Patrón Observer - Observador
    /// </summary>
    public interface IObserver<T>
    {
        void Update(T data);
    }
}
