namespace ElPerrito.Domain.Patterns
{
    /// <summary>
    /// Interfaz del patrón Prototype para clonar objetos
    /// </summary>
    public interface IPrototype<T>
    {
        T Clone();
        T DeepClone();
    }
}
