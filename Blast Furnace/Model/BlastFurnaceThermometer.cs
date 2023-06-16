using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ViewLab10.Model
{
    public class BlastFurnaceThermometer : IObserver, INotifyPropertyChanged
    {
        private BlastFurnace _observable_furnace;

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        private ushort _furanceTemperature;
        public ushort Furnace_temperature
        {
            get
            {
                return _furanceTemperature;
            }
            private set
            {
                _furanceTemperature = value;
                NotifyPropertyChanged("Furnace_temperature");
            }
        }

        public BlastFurnaceThermometer(BlastFurnace observable_furnace)
        {
            _observable_furnace = observable_furnace;
            observable_furnace.Attach(this);
        }

        public void Update(object obj)
        {
            Furnace_temperature = _observable_furnace.Temperature;
            
        }      
    }
}
