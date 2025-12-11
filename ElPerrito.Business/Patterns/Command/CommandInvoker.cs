using System.Collections.Generic;

namespace ElPerrito.Business.Patterns.Command
{
    /// <summary>
    /// Invoker que mantiene historial de comandos para deshacer
    /// </summary>
    public class CommandInvoker
    {
        private readonly Stack<ICommand> _commandHistory = new();

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            _commandHistory.Push(command);
        }

        public void UndoLastCommand()
        {
            if (_commandHistory.Count > 0)
            {
                var command = _commandHistory.Pop();
                command.Undo();
            }
        }

        public int GetHistoryCount() => _commandHistory.Count;
    }
}
