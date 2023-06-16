using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ViewLab10.Model;
using ViewLab10.View;
using System.ComponentModel;
using System.Windows;

namespace ViewLab10.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {

        private BlastFurnace _furance;
        private BlastFurnaceThermometer _thermometer;
        private BlastFurnaceSmeltingTimer _stopwatch;
        private BlastFurnaceStateDisplayer _stateDisplayer;

        #region Views
        private UserControlMainViewModel _userControl;
        private ThermometrViewModel _thermometrViewModel;
        #endregion
        public MainViewModel()
        {
            _furance = new BlastFurnace();
            _stateDisplayer = new BlastFurnaceStateDisplayer(_furance);
            _stopwatch = new BlastFurnaceSmeltingTimer(_furance);
            _thermometer = new BlastFurnaceThermometer(_furance);

            _userControl = new UserControlMainViewModel(_furance, _stopwatch, _stateDisplayer, _thermometer);
            _userControl.PropertyChanged += _userControl_PropertyChanged;

            _thermometrViewModel = new ThermometrViewModel(_furance,_stopwatch, _stateDisplayer, _thermometer);
            _thermometrViewModel.PropertyChanged += _thermometrViewModel_PropertyChanged;
        }
     
        #region Events
        private void _thermometrViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(e.PropertyName);
        }

        private void _userControl_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(e.PropertyName);
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion
        #region UserControlMainViewModel Props
        public ushort MaxTemperature
        {
            get
            {
                return _userControl.MaxTemperature;
            }
            set
            {
                _userControl.MaxTemperature = value;
            }
        }

        public RelayCommand TurnOnCommand => _userControl.TurnOnCommand;
        public RelayCommand TurnOffCommand => _userControl.TurnOffCommand;

        public bool IsCheckedTherm { get => _userControl.IsCheckedTherm; set => _userControl.IsCheckedTherm = value; }
        public Visibility ThermometrIsVisible { get => _userControl.ThermometrIsVisible; }

        public bool IsCheckedWatch { get => _userControl.IsCheckedWatch; set => _userControl.IsCheckedWatch = value; }
        public Visibility StopWatchIsVisible { get => _userControl.StopWatchIsVisible; }

        public bool IsCheckedState { get => _userControl.IsCheckedState; set => _userControl.IsCheckedState = value; }
        public Visibility StateIsVisible { get => _userControl.StateIsVisible; }

        public bool IsOnline => _userControl.IsOnline;
        #endregion
        #region ThermometrViewModel Props
        public ushort Temperature
        {
            get => _thermometrViewModel.Temperature;
        }
        public double AngleOfThermometr
        {
            get => _thermometrViewModel.AngleOfThermometr;
        }
        public string WorkTime { get => _thermometrViewModel.WorkTime; }
        public string OfflineStateColor { get => _thermometrViewModel.OfflineStateColor; }
        public string HeatingStateColor { get => _thermometrViewModel.HeatingStateColor; }
        public string MaintainingStateColor { get => _thermometrViewModel.MaintainingStateColor; }
        public string CoolingStateColor { get => _thermometrViewModel.CoolingStateColor; }

        #endregion
    }
}
