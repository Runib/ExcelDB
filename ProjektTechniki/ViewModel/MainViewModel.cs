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

        public RelayCommand CreateBaseCommand { get; set; }
      
        public MainViewModel(IMyNavigationService navService)
        {
            navigationService = navService;
            InitCommand();
        }

        private void InitCommand()
        {
            CreateBaseCommand = new RelayCommand(() => { navigationService.NavigateTo(ViewModelLocator.CreateBaseKey); });
        }
    }
}