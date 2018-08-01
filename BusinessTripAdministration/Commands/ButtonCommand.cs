using System;
using System.Diagnostics;
using System.Windows.Input;

namespace BusinessTripAdministration.Commands
{
    internal class ButtonCommand : ICommand
    {
        private readonly Action<object> execute;
        private readonly Predicate<object> canExecute;

        public ButtonCommand(Action<object> execute)
        : this(execute, null)
        {
        }

        public ButtonCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }


        #region ICommand Members

        
        public bool CanExecute(object parameters)
        {
            return canExecute == null ? true : canExecute(parameters);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameters)
        {
            execute(parameters);
        }

        #endregion // ICommand Members
    }
}
