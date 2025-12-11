namespace ElPerrito.Business.Patterns.Command
{
    /// <summary>
    /// Patrón Command - Interfaz de comando
    /// </summary>
    public interface ICommand
    {
        void Execute();
        void Undo();
        string GetDescription();
    }
}
