namespace ElPerrito.Business.Patterns.Command
{
    public interface ICommand
    {
        void Execute();
        void Undo();
        string GetDescription();
    }
}
