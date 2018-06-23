using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using ProjektTechniki.Services;
using ProjektTechniki.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjektTechniki.ViewModel
{
    /// <summary>
    /// Klasa widoku CreateBaseAddColumnView
    /// Jej zadaniem jest przetworzenie danych pobranych z widoku, tzn nazw kolumn oraz typów
    /// Dodaje kolumy do wcześniej utworzonej tabeli
    /// </summary>
    public class CreateBaseAddColumnsViewModel :ViewModelBase
    {
        /// <summary>
        /// Zmienna służąca do nawigowania przejściami między stronami danego okna
        /// </summary>
        private IMyNavigationService navigationService;

        /// <summary>
        /// Zmienna informująca czy kolumna o danej nazwie juz zostałą dodana
        /// </summary>
        int add = 0;

        /// <summary>
        /// Deklaracja komendy Dodawania nazw kolumn, uwzglednia czy wszystkie pola zostaly wypełnione
        /// Jeśli wszystkie pola są wypełnione, wyświetla informację o pozytywnym dodaniu kolumny
        /// </summary>
        public RelayCommand AddColumnCommand { get; set; }

        /// <summary>
        /// Deklaracja komendy powodującej przejście do dalszego etapu działanie aplikacji, tj okna z wyświetlaniem danych i operacji na tabelach
        /// </summary>
        public RelayCommand GoNextCommand { get; set; }

        /// <summary>
        /// Zmienna informująca czy dodać czy nie dodawać kolumn do pliku, sprawdza wypełnienie wszystkich pól w widoku
        /// </summary>
        private string addOrNot;
        public string AddOrNot
        {
            get { return addOrNot; }
            set { addOrNot = value; RaisePropertyChanged(() => AddOrNot); }
        }

        /// <summary>
        /// Zmienna połączona z TextBox w widoku, pobiera nazwę kolumny
        /// </summary>
        private string columnName;
        public string ColumnName
        {
            get { return columnName; }
            set { columnName = value; RaisePropertyChanged(() => ColumnName); }
        }

        /// <summary>
        /// Zmienna połącozna z ComboBoxem w widoku, pobiera typ kolumny
        /// </summary>
        private string columnType;
        public string ColumnType
        {
            get { return columnType; }
            set { columnType = value; RaisePropertyChanged(() => ColumnType); }
        }

        /// <summary>
        /// Zmienna Typu DaTable pozwala na dodanie nazw kolumn i przekazanie danych dalej do widoku
        /// </summary>
        private DataTable dt;
        public DataTable Dt
        {
            get { return dt; }
            set { dt = value; RaisePropertyChanged(() => Dt); }
        }

        /// <summary>
        /// Konstruktor klasy
        /// Pobiera zmienną przejścia do nastepnego widoku
        /// inicjuje obiekt Dt oraz ustawia zmienną add na 1
        /// Wywołuje metodę InitCommand
        /// </summary>
        /// <param name="navService">parametr informujący o przejściu do dalszego widoku strony</param>
        public CreateBaseAddColumnsViewModel(IMyNavigationService navService)
        {
            this.navigationService = navService;
            Dt = new DataTable();
            add = 1;
            InitCommand();
        }
        
        /// <summary>
        /// Metoda InitCommand
        /// Zawiera inicjalizacje koimend występujacych w połączeniu widoku i modelu
        /// Są to komendy dodawania kolumn do zmiennej,sprawdzania poprawnosci wypełnienia pól w widoku
        /// oraz komenda przejścia do następnego okna w aplikacji i dodanie kolumn do pliku
        /// </summary>
        private void InitCommand()
        {
            AddColumnCommand = new RelayCommand(() =>
            {
                
                if (ColumnName == null || ColumnName.FirstOrDefault() >= '0' && ColumnName.FirstOrDefault() <= '9' || ColumnName=="")
                {

                    MessageBoxResult result = MessageBox.Show("Podaj prawidłową nazwę",
                        "Confirmation", MessageBoxButton.OK);
                    AddOrNot = "";
                }
                else if (ColumnType==null)
                {
                    MessageBox.Show("Wybierz typ kolumny",
                        "Confirmation", MessageBoxButton.OK);
                }
                else
                {
                    for (int indexName = 0; indexName < Dt.Columns.Count; indexName++)
                    {
                        if (ColumnName == Dt.Columns[indexName].ToString())
                        {
                            add = 0;
                            break;
                        }
                        else
                            add = 1;
                    }
                    if (add == 0)
                    {
                        MessageBoxResult result = MessageBox.Show("Taka kolumna juz istnieje",
                        "Confirmation", MessageBoxButton.OK);
                        ColumnName = "";
                        AddOrNot = "";
                    }
                    else if (add == 1)
                    {
                        Dt.Columns.Add(ColumnName);
                        CellType cellType;
                        if (ColumnType == "String")
                        {
                            cellType = CellType.String;
                        }
                        else if (ColumnType == "Numeryczny")
                        {
                            cellType = CellType.Numeric;
                        }
                        else
                        {
                            cellType = CellType.Boolean;
                        }
                        ViewModelLocator.ColumnType.Add(cellType);
                        ColumnName = "";
                        AddOrNot = "Dodano";
                        ColumnType = null;
                    }
                }
            });

            GoNextCommand = new RelayCommand(() =>
            {
                string path = navigationService.Parameter.ToString();
                FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
                IWorkbook woorkbook = new XSSFWorkbook(stream);
                stream.Close();
                string SheetName = woorkbook.GetSheetName(0);
                ISheet sheet = woorkbook.GetSheet(SheetName);
                IRow row = sheet.CreateRow(0);
                
                
                for (int j = 0; j <Dt.Columns.Count; j++)
                {
                    var cell = row.CreateCell(j);
                    cell.SetCellValue(Dt.Columns[j].ToString());
                }


                stream = new FileStream(path, FileMode.Create, FileAccess.Write);
                woorkbook.Write(stream);

                stream.Close();

                AddOrNot = "";
                Dt.Clear();
                App.Current.MainWindow.Hide();
                ViewModelLocator.Param = path;
                ActionLoadedBase ActionWindow = new ActionLoadedBase();
                ActionWindow.ShowDialog();

                App.Current.MainWindow.Show();
            });
        }
    }
}
