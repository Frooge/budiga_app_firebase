using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace budiga_app.Core
{
    class ObeservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
