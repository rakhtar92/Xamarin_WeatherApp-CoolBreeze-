using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CoolBreeze.Common
{
    /*
     * Relaycommand's sole purpose is to relay(receive or pass information or message) its functionality
     * to other objects by invoking delegates.
     * The default return value for CanExecuteMethod is true
     * OnCanExecuteChanged needs to be called whenever CanExecute is expected to returna  different value
     */
    public class RelayCommands : ICommand
    {
        private readonly Action execute;
        private readonly Func<bool> canExecute;
        public RelayCommands(Action execute) : this(execute, null)
        {

        }
        public RelayCommands(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
            this.execute = execute;
            this.canExecute = canExecute;
        }
        public event EventHandler CanExecuteChanged;
        /*
         * Determines whether RelayCommand can execute in its current state
         */
        /*
         * parameter: Data used by the command. If the command does not require
         * data to be passed, this object can be set to null.
         * returns true if this command can be executed; otherwise, false.
        */
        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute();
        }

        /*
         * 
         * 
         */
        public void Execute(object parameter)
        {
            this.execute();
        }
        public void OnCanExecuteChanged()
        {
            var handler = this.CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
