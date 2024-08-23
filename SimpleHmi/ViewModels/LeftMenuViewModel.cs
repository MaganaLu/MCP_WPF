using Prism.Commands;
using Prism.Regions;
using SimpleHmi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleHmi.ViewModels
{
    class LeftMenuViewModel
    {
        public ICommand NavigateToMainPageCommand { get; private set; }

        public ICommand NavigateToMCP01PageCommand { get; private set; }
        public ICommand NavigateToMCP02PageCommand { get; private set; }
        public ICommand NavigateToMCP03PageCommand { get; private set; }
        public ICommand NavigateToMCP04PageCommand { get; private set; }
        public ICommand NavigateToMCP05PageCommand { get; private set; }
        public ICommand NavigateToMCP06PageCommand { get; private set; }
        public ICommand NavigateToMCP07PageCommand { get; private set; }

        private readonly IRegionManager _regionManager;

        public LeftMenuViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            NavigateToMainPageCommand = new DelegateCommand(() => NavigateTo("MainPage"));
            NavigateToMCP01PageCommand = new DelegateCommand(() => NavigateTo("MCP01Page"));
            NavigateToMCP02PageCommand = new DelegateCommand(() => NavigateTo("MCP02Page"));
            NavigateToMCP03PageCommand = new DelegateCommand(() => NavigateTo("MCP03Page"));
            NavigateToMCP04PageCommand = new DelegateCommand(() => NavigateTo("MCP04Page"));
            NavigateToMCP05PageCommand = new DelegateCommand(() => NavigateTo("MCP05Page"));
            NavigateToMCP06PageCommand = new DelegateCommand(() => NavigateTo("MCP06Page"));
            NavigateToMCP07PageCommand = new DelegateCommand(() => NavigateTo("MCP07Page"));
        }

        private void NavigateTo(string url)
        {
            _regionManager.RequestNavigate(Regions.ContentRegion, url);
        }
    }
}
