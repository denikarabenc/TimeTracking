using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Common.WPFCommand
{
    public class RelayCommand : ICommand
    {
        private Action<object?> execute;
        private Predicate<object?>? canExecute;

        public RelayCommand(Action<object?> execute)
        {
            this.execute = execute;
        }

        public RelayCommand(Action<object?> execute, Predicate<object?>? canExecute) : this(execute)
        {
            this.canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return canExecute != null ? canExecute(parameter) : true;
        }

        public void Execute(object? parameter)
        {
            execute(parameter);
        }
    }
}
