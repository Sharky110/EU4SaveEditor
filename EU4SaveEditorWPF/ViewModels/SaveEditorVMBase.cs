using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EU4SaveEditorWPF.ViewModels
{
    class SaveEditorVMBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName]string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue)) 
                return false;
            field = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            return true;
        }
    }
}
