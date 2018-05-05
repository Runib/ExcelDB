using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using ProjektTechniki.Services;

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
    }
}