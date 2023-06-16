using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewLab10.Model;
using System.Timers;

namespace ViewLab10.ViewModel
{
    public class ThermometrViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private BlastFurnaceThermometer _thermometer;
        private BlastFurnaceSmeltingTimer _stopwatch;
        private BlastFurnaceStateDisplayer _stateDisplayer;
        private BlastFurnace _furance;

        private Timer _timer;
        public ThermometrViewModel(BlastFurnace furnace, BlastFurnaceSmeltingTimer stopwatch, BlastFurnaceStateDisplayer displayer, BlastFurnaceThermometer thermometer)
        {
            _thermometer = thermometer;
            _thermometer.PropertyChanged += _thermometer_PropertyChanged;
            _stopwatch = stopwatch;
            _stateDisplayer = displayer;
            _stateDisplayer.PropertyChanged += StateChanged;
            _furance = furnace;
            _furance.PropertyChanged += _furance_MaxTempChanged;
            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Elapsed += UpdateWorkTime;
            _timer.Start();
            OfflineStateColor = _activeColor;
            HeatingStateColor = _inactiveColor;
            MaintainingStateColor = _inactiveColor;
            CoolingStateColor = _inactiveColor;
        }

              
        #region Thermometr
        private void _furance_MaxTempChanged(object? sender, PropertyChangedEventArgs e)
        {
            _maxTemperature = _furance.Max_temperature;
            _angleParam = 180 / (double)_maxTemperature;
        }

        private void _thermometer_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            AngleOfThermometr = 5;
            NotifyPropertyChanged("Temperature");
        }
        private ushort _maxTemperature;
        private double _angleParam;
        public ushort Temperature
        {
            get { return _thermometer.Furnace_temperature; }          
        }
        private double _angleOfThermometr;
        public double AngleOfThermometr
        {
            get
            {
                return _angleOfThermometr;
            }
            private set
            {
                _angleOfThermometr = Temperature* _angleParam;
                NotifyPropertyChanged("AngleOfThermometr");
            }
        }
        #endregion
        #region Stopwatch
        private void UpdateWorkTime(object? sender, ElapsedEventArgs e)
        {
            NotifyPropertyChanged("WorkTime");
        }
        public string WorkTime 
        { 
            get
            {
                return _stopwatch.GetWorkTime();
            }
        }
        #endregion
        private void StateChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (_stateDisplayer.CurrentState)
            {
                case 0: 
                    OfflineStateColor = _activeColor; 
                    HeatingStateColor = _inactiveColor;
                    MaintainingStateColor = _inactiveColor;
                    CoolingStateColor = _inactiveColor;
                    break;   
                case 1: 
                    OfflineStateColor = _inactiveColor; 
                    HeatingStateColor = _activeColor;
                    MaintainingStateColor = _inactiveColor;
                    CoolingStateColor = _inactiveColor;
                    break;   
                case 2: 
                    OfflineStateColor = _inactiveColor; 
                    HeatingStateColor = _inactiveColor;
                    MaintainingStateColor = _activeColor;
                    CoolingStateColor = _inactiveColor;
                    break;    
                case 3: 
                    OfflineStateColor = _inactiveColor; 
                    HeatingStateColor = _inactiveColor;
                    MaintainingStateColor = _inactiveColor;
                    CoolingStateColor = _activeColor;
                    break;           
            }
        }
        private string _activeColor = "#FF82D4FF";
        private string _inactiveColor = "#FFCF5D1D";
        private string _offlineStateColor;
        public string OfflineStateColor
        {
            get => _offlineStateColor;
            private set
            {
                _offlineStateColor= value;
                NotifyPropertyChanged("OfflineStateColor");
            }
        }
        private string _heatingStateColor;
        public string HeatingStateColor
        {
            get => _heatingStateColor;
            private set
            {
                _heatingStateColor = value;
                NotifyPropertyChanged("HeatingStateColor");
            }
        } 
        private string _maintainingStateColor;
        public string MaintainingStateColor
        {
            get => _maintainingStateColor;
            private set
            {
                _maintainingStateColor = value;
                NotifyPropertyChanged("MaintainingStateColor");
            }
        }  
        private string _coolingStateColor;
        public string CoolingStateColor
        {
            get => _coolingStateColor;
            private set
            {
                _coolingStateColor = value;
                NotifyPropertyChanged("CoolingStateColor");
            }
        }
    }
}
