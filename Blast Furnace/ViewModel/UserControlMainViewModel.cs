using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ViewLab10.Model;
using ViewLab10.View;

namespace ViewLab10.ViewModel
{
    public class UserControlMainViewModel : INotifyPropertyChanged
    {
        private BlastFurnace _furance;
        private BlastFurnaceThermometer _thermometer;
        private BlastFurnaceSmeltingTimer _stopwatch;
        private BlastFurnaceStateDisplayer _stateDisplayer;

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public UserControlMainViewModel(BlastFurnace furnace, BlastFurnaceSmeltingTimer stopwatch, BlastFurnaceStateDisplayer displayer, BlastFurnaceThermometer thermometer)
        {
            _furance = furnace;
            _furanceIsOnline = false;
            _thermometrIsVisible = Visibility.Visible;
            _isCheckedState = true;
            _isCheckedTherm = true;
            _isCheckedWatch = true;
            _thermometer = thermometer;
            _stateDisplayer = displayer;
            _stopwatch = stopwatch;
        }

        private bool _furanceIsOnline;
        public bool IsOnline { get => _furanceIsOnline; }
        public ushort MaxTemperature
        {
            get
            {
                return _furance.Max_temperature;
            }
            set
            {
                if (value <= 2000)
                {
                    _furance.Max_temperature = value;
                    NotifyPropertyChanged("MaxTemperature");
                }
                else _furance.Max_temperature = 2000;
            }
        }
        private RelayCommand turnOnCommand;
        public RelayCommand TurnOnCommand
        {
            get
            {
                return turnOnCommand ??
                  (turnOnCommand = new RelayCommand(obj =>
                  {
                      _furanceIsOnline = true;
                      _furance.Forward();
                      NotifyPropertyChanged("IsOnline");
                  }));
            }
        }
        private RelayCommand turnOffCommand;
        public RelayCommand TurnOffCommand
        {
            get
            {
                return turnOffCommand ??
                  (turnOffCommand = new RelayCommand(obj =>
                  {
                      _furanceIsOnline = false;
                      _furance.Backward();
                      NotifyPropertyChanged("IsOnline");
                  }));
            }
        }
        #region Thermometr

        private bool _isCheckedTherm;
        public bool IsCheckedTherm
        {
            get { return _isCheckedTherm; }
            set
            {
                if (value)
                {
                    ThermometrIsVisible = Visibility.Visible;
                    _furance.Attach(_thermometer);
                }
                else
                {
                    ThermometrIsVisible = Visibility.Hidden;
                    _furance.Detach(_thermometer);
                }
                _isCheckedTherm = value;
                NotifyPropertyChanged("ThermometrIsVisible");
            }
        }
        private Visibility _thermometrIsVisible;
        public Visibility ThermometrIsVisible
        {
            get
            {
                return _thermometrIsVisible;
            }
            private set
            {
                _thermometrIsVisible = value;
            }
        }
        #endregion
        #region StopWatch

        private bool _isCheckedWatch;
        public bool IsCheckedWatch
        {
            get { return _isCheckedWatch; }
            set
            {
                if (value)
                {
                    StopWatchIsVisible = Visibility.Visible;
                    _furance.Attach(_stopwatch);
                }
                else
                {
                    StopWatchIsVisible = Visibility.Hidden;
                    _furance.Detach(_stopwatch);
                }
                _isCheckedWatch = value;
                NotifyPropertyChanged("StopWatchIsVisible");
            }
        }
        private Visibility _stopWatchIsVisible;
        public Visibility StopWatchIsVisible
        {
            get
            {
                return _stopWatchIsVisible;
            }
            private set
            {
                _stopWatchIsVisible = value;
            }
        }
        #endregion   
        #region State

        private bool _isCheckedState;
        public bool IsCheckedState
        {
            get { return _isCheckedState; }
            set
            {
                if (value)
                {
                    StateIsVisible = Visibility.Visible;
                    _furance.Attach(_stateDisplayer);
                }
                else
                {
                    StateIsVisible = Visibility.Hidden;
                    _furance.Detach(_stateDisplayer);
                }
                _isCheckedState = value;
                NotifyPropertyChanged("StateIsVisible");
            }
        }
        private Visibility _stateIsVisible;
        public Visibility StateIsVisible
        {
            get
            {
                return _stateIsVisible;
            }
            private set
            {
                _stateIsVisible = value;
            }
        }
        #endregion

    }
}
