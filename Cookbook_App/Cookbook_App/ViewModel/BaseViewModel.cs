using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LaCucina.ViewModel
{
    public abstract class BaseViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
