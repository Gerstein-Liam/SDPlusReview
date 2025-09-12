using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF.Commands.Generics
{
    public abstract class AsyncCommandBase : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private bool _isExecuting;
        private bool IsExecuting
        {
            get
            {
                return _isExecuting;
            }
            set
            {
                _isExecuting = value;
                RaiseCanExecuteChanged();
            }
        }

        private bool _isAllowedToExecute =true;
        public bool IsAllowedToExecute
        {
            get
            {
                return _isAllowedToExecute;
            }
            set
            {
                _isAllowedToExecute = value;
                RaiseCanExecuteChanged();
            }
        }

     

        public virtual bool CanExecute(object? parameter)
        {
            return !IsExecuting && IsAllowedToExecute;
        }
        public void AllowExecution(bool allow)
        {


            IsAllowedToExecute = allow;
        }

        public abstract  Task TaskToExecute();
      
        public async void Execute(object? parameter)
        {
            IsExecuting = true; 
            await TaskToExecute();
            IsExecuting= false;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
