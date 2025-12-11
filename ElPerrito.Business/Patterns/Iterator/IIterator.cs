namespace ElPerrito.Business.Patterns.Iterator
{
    /// <summary>
    /// Patrón Iterator - Interfaz del iterador
    /// </summary>
    public interface IIterator<T>
    {
        bool HasNext();
        T Next();
        void Reset();
    }
}
