using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using ProjektTechniki.Services;
using ProjektTechniki.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektTechniki.ViewModel 
{
    public class LoadBaseViewModel : ViewModelBase
    {
        private IMyNavigationService navigationService;
     
        public RelayCommand LoadBaseCommand { get; set; }
        public RelayCommand AcceptBaseCommand { get; set; }

        

        private string baseName;
        public string BaseName
        {
            get { return baseName; }
            set { baseName = value; RaisePropertyChanged(() => BaseName); }
        }

        public LoadBaseViewModel(IMyNavigationService navService)
        {
            navigationService = navService;
            InitCommand();
        }

        private void InitCommand()
        {
            LoadBaseCommand = new RelayCommand(() => {
                var dialog = new OpenFileDialog()
                {
                    //TODO: filters
                };
                dialog.DefaultExt = ".xls";
<<<<<<< HEAD
                dialog.Filter = "Excel file (*.xls)|*.xls|Excel file (*.xlsx)|*.xlsx|Excel files (*.xls;*.xlsx)|*.xls;*.xlsx";
=======
                dialog.Filter = "Excel files (*.xls)|*.xls";

>>>>>>> 147a88c... Addded Load, Create and Displaydupadupacycki
                if (dialog.ShowDialog() == true)
                {
                    BaseName = dialog.FileName;
                }
            });

            AcceptBaseCommand = new RelayCommand(() => {
                App.Current.MainWindow.Hide();
                ViewModelLocator.Param = BaseName;
                ActionLoadedBase ActionWindow = new ActionLoadedBase();
                ActionWindow.ShowDialog();

                App.Current.MainWindow.Show();
                });
        }
    }
}
