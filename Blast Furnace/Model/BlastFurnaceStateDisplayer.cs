using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ViewLab10.Model
{
    public class BlastFurnaceStateDisplayer : IObserver, INotifyPropertyChanged
    {
        private BlastFurnace _observable_furnace;
        private byte _furnace_state;

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        public BlastFurnaceStateDisplayer(BlastFurnace observable_furnace)
        {
            _observable_furnace = observable_furnace;
            observable_furnace.Attach(this);
        }

        public void Update(object obj)
        {
            CurrentState = (byte)_observable_furnace._current_state;
        }
        public byte CurrentState
        {
            get { return _furnace_state; }
            set
            {
                _furnace_state = value;
                NotifyPropertyChanged("CurrentState");
            }
        }
       
    }
}
