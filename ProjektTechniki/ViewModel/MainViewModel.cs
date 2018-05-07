using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using ProjektTechniki.Services;
using CommonServiceLocator;

namespace ProjektTechniki.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IMyNavigationService navigationService;

        public RelayCommand CreateTableCommand { get; set; }
      
        public MainViewModel(IMyNavigationService navService)
        {
            navigationService = navService;
            InitCommand();
        }

        private void InitCommand()
        {
            CreateTableCommand = new RelayCommand(() => { navigationService.NavigateTo(ViewModelLocator.CreateTableKey); });
        }
    }
}