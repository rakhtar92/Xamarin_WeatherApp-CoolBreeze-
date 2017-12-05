using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CoolBreeze.Common
{
    public abstract class ObservableBase:INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        /*
         * Checks if the property already matches a desired value.
         * Sets the property and notifies the listeners only when necessary.
         * PARAMETERS:
         *          storage: Type T references to a property with both geterand setter.
         *          value: Type T Desired value of the property.
         *          propertyName: Type String, Name of the property to notify the listeners.
         *                          This value is optional and can be providedd when invoked from compilers that support CallerMemberName.
         */
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (object.Equals(storage, value))
                return false;

            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }

        /*
         * Notifies listeners that a property value has been changed.
         */
         protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var eventHandler = this.PropertyChanged;
            if(eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
