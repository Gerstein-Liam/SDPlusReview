using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF.Commands.Generics
{
    public class ActionCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly Action _FctToExecute;

        public ActionCommand(Action fctToExecute)
        {
            _FctToExecute = fctToExecute;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _FctToExecute?.Invoke();
        }
    }
}
