namespace ElPerrito.Domain.Patterns
{
    public interface IPrototype<T>
    {
        T Clone();
        T DeepClone();
    }
}
