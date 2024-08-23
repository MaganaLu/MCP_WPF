using Prism.Mvvm;
using Prism.Regions;
using SimpleHmi.PlcService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHmi.ViewModels
{
    class SettingsPageViewModel :BindableBase, INavigationAware
    {
        public int InletPumpSpeed
        {
            get { return _inletPumpSpeed; }
            set { SetProperty(ref _inletPumpSpeed, value); }
        }
        private int _inletPumpSpeed;

        public int OutletSpeed
        {
            get { return _outletSpeed; }
            set { SetProperty(ref _outletSpeed, value); }
        }
        private int _outletSpeed;

        private readonly IPlcService _ABPlcService;

        public SettingsPageViewModel(IPlcService s7PlcService)
        {
            _ABPlcService = s7PlcService;
            this.PropertyChanged += OnPropertyChanged;
        }

        private async void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(OutletSpeed))
            {
                await _ABPlcService.WriteSpeedOutletPump((short)OutletSpeed);
            }
            else if (e.PropertyName == nameof(InletPumpSpeed))
            {
                await _ABPlcService.WriteSpeedInletPump((short)InletPumpSpeed);
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            InletPumpSpeed = _ABPlcService.InletPumpSpeed;
            OutletSpeed = _ABPlcService.OutletPumpSpeed;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }
    }
}
