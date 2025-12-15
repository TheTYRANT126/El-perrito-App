namespace ElPerrito.Business.Patterns.Observer
{
    public interface IObserver<T>
    {
        void Update(T data);
    }
}
