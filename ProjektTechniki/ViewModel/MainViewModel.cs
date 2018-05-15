using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using ProjektTechniki.Services;
using CommonServiceLocator;
using ProjektTechniki.View;

namespace ProjektTechniki.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IMyNavigationService navigationService;

        public RelayCommand CreateBaseCommand { get; set; }
        public RelayCommand LoadBaseCommand { get; set; }
        public RelayCommand OnLoad { get; set; }

        public MainViewModel(IMyNavigationService navService)
        {
            navigationService = navService;
            InitCommand();
        }

        private void InitCommand()
        {
            OnLoad = new RelayCommand(() =>
            {
               
            });
            CreateBaseCommand = new RelayCommand(() => { navigationService.NavigateTo(ViewModelLocator.CreateBaseKey); });
            LoadBaseCommand = new RelayCommand(() => { navigationService.NavigateTo(ViewModelLocator.LoadBaseKey); });
        }
    }
}