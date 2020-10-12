using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ATMApp.Command
{
    class DelegateCommand<T> : ICommand
    {
        private readonly Action<object> action;
        public DelegateCommand(Action<object> _action)
        {
            action = _action;
        }
       public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action(parameter);
        }
    }
}
