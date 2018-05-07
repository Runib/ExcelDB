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
        public RelayCommand NavigateCommand { get; set; }
      
        public MainViewModel()
        {
            
        }

       

      
    }
}