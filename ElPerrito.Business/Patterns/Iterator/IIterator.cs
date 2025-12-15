namespace ElPerrito.Business.Patterns.Iterator
{
    public interface IIterator<T>
    {
        bool HasNext();
        T Next();
        void Reset();
    }
}
