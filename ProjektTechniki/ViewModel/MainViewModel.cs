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
            var navService = SimpleIoc.Default.GetInstance<IMyNavigationService>();
            NavigateCommand = new RelayCommand(() => navService.NavigateTo(ViewModelLocator.CreateTableKey));
        }

        private string text="1212312";
        public string Text
        {
            get { return text; }
            set { text = value; RaisePropertyChanged(() => Text); }
        }

      

        public void ShowFirstView()
        {
            ServiceLocator.Current.GetInstance<MyNavigationService>().NavigateTo("FirstPageViewModel");
        }
    }
}