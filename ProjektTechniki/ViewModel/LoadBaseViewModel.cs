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
    /// <summary>
    /// Klasa połączona z widokiem LoadBaseView
    /// Odpowiedzialna za wybranie pliku z komputera i zapamiętanie ścieżki gdzie on sie znajduje
    /// </summary>
    public class LoadBaseViewModel : ViewModelBase
    {

        /// <summary>
        /// Zmienna nawigująca pomiędzy widokami
        /// </summary>
        private IMyNavigationService navigationService;
     
        /// <summary>
        /// Deklaracja komendy wywoływanej podczas wciśnięcia odpowiedniego przycisku
        /// Jej zadaniem jest zapamiętanie ścieżki do pliku wybranego przez użytkownika
        /// </summary>
        public RelayCommand LoadBaseCommand { get; set; }

        /// <summary>
        /// Komendy wywoływana podczas zatwierdzenia wyboru użytkownika
        /// powoduje przejście do kolejnego widoku applikacji
        /// </summary>
        public RelayCommand AcceptBaseCommand { get; set; }

        /// <summary>
        /// Zmienna przechowująca scięzkę do pliku
        /// </summary>
        private string baseName;
        public string BaseName
        {
            get { return baseName; }
            set { baseName = value; RaisePropertyChanged(() => BaseName); }
        }

        /// <summary>
        /// Konstruktor klasy, ustawia zmienna nawigującą oraz wywołuje metodę InitCommand
        /// </summary>
        /// <param name="navService"></param>
        public LoadBaseViewModel(IMyNavigationService navService)
        {
            navigationService = navService;
            InitCommand();
        }

        /// <summary>
        /// Metoda zawierająca inicjalizację komend opisanych wyżej
        /// </summary>
        private void InitCommand()
        {
            LoadBaseCommand = new RelayCommand(() => {
                var dialog = new OpenFileDialog()
                {
                    //TODO: filters
                };
                dialog.DefaultExt = ".xls";
                dialog.Filter = "Excel file (*.xls)|*.xls|Excel file (*.xlsx)|*.xlsx|Excel files (*.xls;*.xlsx)|*.xls;*.xlsx";

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
