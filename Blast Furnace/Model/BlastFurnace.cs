using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.ComponentModel;

namespace ViewLab10.Model
{
    public class BlastFurnace: SimpleStateMachine<int, int>, IObservable, INotifyPropertyChanged
    {
        private List<IObserver> observers;
        private Timer _heating_timer;
        private ushort _maxTemp;
        public ushort Max_temperature
        {
            get
            {
                return _maxTemp;
            }
            set
            {
                _maxTemp = value;
                if (Temperature > Max_temperature) 
                { 
                    Temperature = Max_temperature;
                    Temperature--;
                }
                NotifyPropertyChanged("Max_temperature");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        public ushort Temperature { get; private set; }

        public BlastFurnace() : base(BlastFurnaceInstructions.instructions, (int)FurnaceStates.Offline)
        {
            observers = new List<IObserver>();
            _heating_timer = new System.Timers.Timer();
            _heating_timer.Interval = 15;
            _heating_timer.Elapsed += ChangeTemperature;
            _heating_timer.AutoReset = true;
        }
        
        public void Attach(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (IObserver o in observers) 
            {
                o.Update(_current_state);
            }
        }

        public void Forward()
        {
            InputSymbol((int)FurnaceControls.Forward);
            if (_current_state == (int)FurnaceStates.Heating) _heating_timer.Enabled = true;
            else _heating_timer.Enabled = false;
            Notify();            
        }

        public void Backward() 
        {
            InputSymbol((int)FurnaceControls.Backward);
            if (_current_state == (int)FurnaceStates.Cooling) _heating_timer.Enabled = true;
            else _heating_timer.Enabled = false;
            Notify();
        }

        private void ChangeTemperature(Object source, System.Timers.ElapsedEventArgs e)
        {
            if (_current_state == (int)FurnaceStates.Heating)
            {
                if (Temperature >= Max_temperature)
                {
                    _heating_timer.Enabled = false;
                    this.Forward();
                }
                else Temperature++;
            }
            if (_current_state == (int)FurnaceStates.Cooling)
            {
                if (Temperature <= 0)
                {
                    _heating_timer.Enabled = false;
                    this.Backward();
                }
                else Temperature--;
            }                        
            Notify();
        }        
    }
}
