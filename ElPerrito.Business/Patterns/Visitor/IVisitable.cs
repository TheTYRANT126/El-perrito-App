namespace ElPerrito.Business.Patterns.Visitor
{
    public interface IVisitable
    {
        void Accept(IVisitor visitor);
    }
}
