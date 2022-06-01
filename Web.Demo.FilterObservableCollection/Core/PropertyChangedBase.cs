using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Web.Demo.FilterObservableCollection.Core
{
    internal class PropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Sets a new value to the field variable and raises the PropertyChanged event, only if current value != new value.
        /// </summary>
        /// <typeparam name="T">The type of the property (can be omitted from call if string or int for example)</typeparam>
        /// <param name="field">The field member variable associated with a property.</param>
        /// <param name="newValue">The new value to be assigned to the field member.</param>
        /// <param name="propertyName">The name of the property that wraps the field member.</param>
        /// <returns>Returns true if the event is raised; otherwise returns false.</returns>
        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            try
            {
                //Compare the current and new values, and raise the event in case they're different.
                if (!EqualityComparer<T>.Default.Equals(field, newValue))
                {
                    field = newValue;
                    this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Form Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return false;
        }
    }
}