using System;
using System.Windows.Input;

namespace TimeView.wpf.Utility
{
    public class CustomCommand : ICommand
    {
        private readonly Predicate<object> _canExecute;

        private readonly Action<object> _execute;

        public CustomCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            var b = _canExecute == null ? true : _canExecute(parameter);
            return b;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}