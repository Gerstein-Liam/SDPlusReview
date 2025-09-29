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

        private readonly Predicate<object> _canExecute;




        public void RaiseCanExecuteChanged() { 
        
        
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
        
        public ActionCommand(Action fctToExecute)
        {
            _FctToExecute = fctToExecute;
        }

        public ActionCommand(Action fctToExecute, Predicate<object> canExecutePredicate)
        {
            _FctToExecute = fctToExecute;
            _canExecute = canExecutePredicate;
        }

        public bool CanExecute(object? parameter)
        {

            if (_canExecute == null)
            {

                return true;
            }
            else
            {
                return true && _canExecute.Invoke(null);
            
            }
            
         
        }

        public void Execute(object? parameter)
        {
            _FctToExecute?.Invoke();
        }
    }
}
