using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.CommandWpf;
using ProjektTechniki.Services;
using System.Data;
using ExcelLibrary.SpreadSheet;
using GalaSoft.MvvmLight;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;
using System.Windows;

namespace ProjektTechniki.ViewModel
{
    /// <summary>
    /// Klasa widoku CreateBaseView, odpowiedzialna za tworzenie pliku i jednego arkusza 
    /// o nazwach i rozszerzeniu podanym przez użytkownika
    /// </summary>
    public class CreateBaseViewModel : ViewModelBase
    {
        /// <summary>
        /// Zmienna nawigująca do kolejnego widoku
        /// </summary>
        private IMyNavigationService navigationService;

        /// <summary>
        /// Deklaracja komendy odpowiedzialnej za stworzenie bazy o podanych parametrach przez uzytkownika
        /// wywoływana podczas wciśnięcia przycisku
        /// Przechodzi do kolejnego widoku
        /// </summary>
        public RelayCommand CreateBaseCommand { get; set; }

        /// <summary>
        /// Zmienna połącozna z polem w widoku, przechowuje nazwę bazy/pliku
        /// </summary>
        private string baseName;

        /// <summary>
        /// Zmienna połącozna z polem w widoku, przechowuje nazwę bazy/pliku
        /// </summary>
        public string BaseName
        {
            get { return baseName; }
            set { baseName = value; RaisePropertyChanged(() => BaseName); }
        }

        /// <summary>
        /// Zmienna połącozna z polem w widoku, przechowuje nazwę tabeli/arkusza
        /// </summary>
        private string tableName;

        /// <summary>
        /// Zmienna połącozna z polem w widoku, przechowuje nazwę tabeli/arkusza
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; RaisePropertyChanged(() => TableName); }
        }

        /// <summary>
        /// Zmienna połącozna z polem w widoku, przechowuje rozszerzenie pliku, .xls lub .xlsx
        /// </summary>
        private string pathName;

        /// <summary>
        /// Zmienna połącozna z polem w widoku, przechowuje rozszerzenie pliku, .xls lub .xlsx
        /// </summary>
        public string PathName
        {
            get { return pathName; }
            set { pathName = value; RaisePropertyChanged(() => PathName); }
        }

        /// <summary>
        /// Konstruktor klasy
        /// Ustawia zmienną nawigującą
        /// Wywołuje metodę InitCommand
        /// </summary>
        /// <param name="navService"></param>
        public CreateBaseViewModel(IMyNavigationService navService)
        {
            navigationService = navService;
            InitCommand();
        }

        /// <summary>
        /// Metoda zawierająca inizjalizację wszystkich komend w widoku
        /// W tym widoku jest tylko jedna komenda opisana wyżej
        /// </summary>
        private void InitCommand()
        {
            CreateBaseCommand = new RelayCommand(() =>
            {
                if (PathName == null)
                {
                    MessageBoxResult result = MessageBox.Show("Wybierz odpowiednie rozszerzenie",
                        "Confirmation", MessageBoxButton.OK);
                }
                else if (BaseName == null || BaseName == "")
                {
                    MessageBoxResult result = MessageBox.Show("Podaj nazwę bazy",
                        "Confirmation", MessageBoxButton.OK);
                }
                else if (TableName == null || TableName == "")
                {
                    MessageBoxResult result = MessageBox.Show("Podaj nazwę arkusza",
                        "Confirmation", MessageBoxButton.OK);
                }
                else if (BaseName.FirstOrDefault() >= '0' && BaseName.FirstOrDefault() <= '9')
                {
                    MessageBoxResult result = MessageBox.Show("Nazwa nie powinna zaczynać się cyfrą\n" +
                        "lub być liczbą",
                        "Confirmation", MessageBoxButton.OK);
                    BaseName = "";
                }
                else if (TableName.FirstOrDefault() >= '0' && TableName.FirstOrDefault() <= '9')
                {
                    MessageBoxResult result = MessageBox.Show("Nazwa nie powinna zaczynać się cyfrą\n" +
                        "lub być liczbą",
                        "Confirmation", MessageBoxButton.OK);
                    TableName = "";
                }
                else
                {
                    string file = $"D:\\{BaseName}.{PathName}";
                    var stream = new FileStream(file, FileMode.Create, FileAccess.Write);
                    //XSSFWorkbook workbook = new XSSFWorkbook();
                    XSSFSheet sheet = (XSSFSheet)(App.workbook.CreateSheet($"{TableName}"));
                    App.workbook.Write(stream);
                    stream.Close();
                    navigationService.NavigateTo(ViewModelLocator.CreateBaseAddColumnsKey, file);
                }
            });
        }


    }
}
