using System.Collections.Generic;

namespace ElPerrito.Business.Patterns.Memento
{
    public class VentaCaretaker
    {
        private readonly Stack<VentaMemento> _history = new();

        public void SaveState(VentaMemento memento)
        {
            _history.Push(memento);
        }

        public VentaMemento? RestoreLastState()
        {
            return _history.Count > 0 ? _history.Pop() : null;
        }

        public int GetHistoryCount() => _history.Count;
    }
}
