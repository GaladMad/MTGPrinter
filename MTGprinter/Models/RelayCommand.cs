using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MTGprinter.Models
{
    public class RelayCommand : ICommand//, IDisposable
    {
        //private readonly Action executedMethod;
        //private readonly Func<T, bool> canExecuteMethod;

        //public event EventHandler CanExecuteChanged;
        //public RelayCommand(Action execute) : this(execute, null) { }

        //public RelayCommand(Action execute, Func<T, bool> canExecute)
        //{
        //    this.executedMethod = execute ?? throw new ArgumentNullException("execute");
        //    this.canExecuteMethod = canExecute;
        //}

        //public bool CanExecute(object parameter) => this.canExecuteMethod == null || this.canExecuteMethod((T)parameter);
        //public void Execute(object parameter) => this.executedMethod((T)parameter);
        //public void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        //------------------------------------------------------------------------------------------------------------------------------

        private Action<object> _action;

        public RelayCommand(Action<object> action)
        {
            _action = action;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter != null)
            {
                _action(parameter);
            }
            else
            {
                _action("Hello World");
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //public event EventHandler CanExecuteChanged;

        //private Func<bool> canExecuteFunc;
        //private Action executeAction;
        //private Action<object> execute;

        //public RelayCommand(Action executeAction) : this(executeAction, () => true)
        //{
        //}

        //public RelayCommand(Action executeAction, Func<bool> canExecuteFunc)
        //{
        //    this.executeAction = executeAction;
        //    this.canExecuteFunc = canExecuteFunc;
        //}

        //public RelayCommand(Action<object> execute)
        //{
        //    this.execute = execute;
        //}

        //public bool CanExecute(object parameter)
        //{
        //    return canExecuteFunc();
        //}

        //public void Dispose()
        //{
        //    RemoveAllEvents();
        //}

        //public void Execute(object parameter)
        //{
        //    executeAction();
        //}

        //public void RemoveAllEvents()
        //{
        //    CanExecuteChanged = null;
        //}
        //------------------------------------------------------------------------------------------------------------------------------
        ///using
        //private RelayCommand commandDoSomething;

        //public RelayCommand CommandDoSomething => commandDoSomething ??
        //       (commandDoSomething = new RelayCommand(
        //       async () =>
        //       {
        //           await .....
        
        //       }
        //       ));

    }
}
