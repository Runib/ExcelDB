using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjektTechniki.ViewModel
{
    /// <summary>
    /// Klasa widoku SearchRecordViewModel
    /// obłsuguje przyciski oraz pola w widoku
    /// Przekazuje wybrane parametry do okna wyświetlania
    /// </summary>
    public class SearchRecordViewModel : ViewModelBase
    {
        /// <summary>
        /// Deklaracja komendy połącoznej z przyciskiem zatwierdzajacym
        /// Zapamietuje wszystkie pobrane dane i zamyka okna widoku
        /// </summary>
        public RelayCommand<object> SearchCommand { get; set; }

        /// <summary>
        /// Komenda, wywoływana podczas odświeżania okna/ładowania okna
        /// </summary>
        public RelayCommand OnLoad { get; set; }

        /// <summary>
        /// Kolekcja zawierająca nazwy Tabel dostępnych z widoku wyswietlania
        /// </summary>
        public ObservableCollection<string> TablesName
        {
            get;
            set;
        }

        /// <summary>
        /// Zmienna przechowująca nazwę kolumny wybrana przez użytkownika
        /// </summary>
        private string selectedName;
        public string SelectedName
        {
            get { return selectedName; }
            set { selectedName = value; RaisePropertyChanged(() => SelectedName); }
        }

        /// <summary>
        /// Zmienna przechowująca tekst wyszukiwany wpisany przez użytkownika
        /// </summary>
        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set { searchText = value; RaisePropertyChanged(() => SearchText); }
        }

        /// <summary>
        /// Zmienna pomagająca pobrać nazwy kolumn
        /// </summary>
        private DataTable table;
        public DataTable Table
        {
            get { return table; }
            set { table = value; RaisePropertyChanged(() => Table); }
        }


        /// <summary>
        /// Konstruktor klasy, inicjalizuje zmienną TablesName oraz wywołuje metodę InitCommand
        /// </summary>
        public SearchRecordViewModel()
        {
            TablesName = new ObservableCollection<string>();
            InitCommand();
        }

        /// <summary>
        /// Metoda zawierająca ciała wszystkich zadeklarowanych komend
        /// Zapamiętuje dane wybrane przez użytkownika, zamyka okno
        /// </summary>
        private void InitCommand()
        {
            SearchCommand = new RelayCommand<object>(w=>
            {
                if (SearchText == "" || SearchText == null)
                {
                    MessageBox.Show("Wpisz wyrażenie",
                        "Confirmation", MessageBoxButton.OK);
                }
                else if (SelectedName == null || SelectedName == "")
                {
                    MessageBox.Show("Wybierz kolumne",
                        "Confirmation", MessageBoxButton.OK);
                }
                else
                {
                    ViewModelLocator.SearchData.Add(SearchText);
                    ViewModelLocator.SearchData.Add(SelectedName);
                    SearchText = "";
                    SelectedName = "";
                    TablesName.Clear();
                    ((Window)w).Close();
                }
            });
            OnLoad = new RelayCommand(() =>
            {
                Init();
            });
        }

        /// <summary>
        /// Metoda pobierająca nazwy kolumn ze zmiennej dostepnej z klasy ViewModelLocator
        /// </summary>
        private void Init()
        {
            Table = new DataTable();
            Table = (DataTable)ViewModelLocator.ColumnsName;
            for (int colIndex = 1; colIndex < Table.Columns.Count; colIndex++)
            {
                TablesName.Add(Table.Columns[colIndex].ToString());
            }
        }
    }
}
