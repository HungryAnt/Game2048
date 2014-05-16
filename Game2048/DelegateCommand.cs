using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Gods.Foundation
{
    public class DelegateCommand : ICommand
    {
        public Action<object> ExecuteAction { get; set; }
        public Func<object, bool> CanExecuteAction { get; set; }

        public DelegateCommand()
        {
            
        }

        public DelegateCommand(Action<object> excuteAction, Func<object, bool> canExecuteAction = null)
        {
            ExecuteAction = excuteAction;
            CanExecuteAction = canExecuteAction;
        }

        public bool CanExecute(object parameter)
        {
            if (CanExecuteAction == null)
            {
                return ExecuteAction != null;
            }
            return CanExecuteAction(parameter);
        }

        public void Execute(object parameter)
        {
            if (ExecuteAction != null)
            {
                ExecuteAction(parameter);
            }
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}
