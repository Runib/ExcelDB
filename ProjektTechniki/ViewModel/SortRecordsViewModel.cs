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
    /// Klasa widoku SortRecordsView
    /// Pobiera dane wybrane przez użytkownika w widoku i podaje dalej do widoku wyświetlania
    /// </summary>
    public class SortRecordsViewModel : ViewModelBase
    {
        /// <summary>
        /// Kolekcja zawierająca nazwy kolumn tabeli
        /// </summary>
        public ObservableCollection<string> TablesName
        {
            get;
            set;
        }

        /// <summary>
        /// Deklaracja komendy wywoływanej podczas kliknięciu przycisku Sortuj, przekazuje wybrane dane przez użytkownika
        /// Dodatkowo zamyka okno widoku
        /// </summary>
        public RelayCommand<object> SortCommand { get; set; }

        /// <summary>
        /// Komenda wywoływana podczas ładowania okna
        /// </summary>
        public RelayCommand OnLoad { get; set; }

        /// <summary>
        /// Zmienna przechowująca tabelę, pomagająca przy pobraniu nazw kolumn tabeli
        /// </summary>
        private DataTable table;
        public DataTable Table
        {
            get { return table; }
            set { table = value; RaisePropertyChanged(() => Table); }
        }

        /// <summary>
        /// Zmienna przechowująca sposób sortowania
        /// </summary>
        private string sortBy;
        public string SortBy
        {
            get { return sortBy; }
            set { sortBy = value; RaisePropertyChanged(() => SortBy); }
        }

        /// <summary>
        /// Zmienna przechowująca wybraną nazwe kolumny którą użytkownik chce posortować
        /// </summary>
        private string selectedName;
        public string SelectedName
        {
            get { return selectedName; }
            set { selectedName = value; RaisePropertyChanged(() => SelectedName); }
        }

        /// <summary>
        /// Kontruktor klasy, inicjalizuje zmienną TablesName oraz wywołuje metodę InitCommand
        /// </summary>
        public SortRecordsViewModel()
        {
            InitCommand();
            TablesName = new ObservableCollection<string>();
        }

        /// <summary>
        /// MEtoda zawierająca ciała komnd zadeklarowanych wcześniej, zapamiętuje wybrany wybór użytkownika i zamyka okno
        /// </summary>
        private void InitCommand()
        {
            SortCommand = new RelayCommand<object>(w =>
            {
                if (SortBy == null || SortBy=="")
                {
                    MessageBoxResult result = MessageBox.Show("Wybierz rodzaj sortowania",
                        "Confirmation", MessageBoxButton.OK);
                }
                else if (SelectedName == null || SelectedName=="")
                {
                    MessageBoxResult result = MessageBox.Show("Wybierz kolumne wedlug ktorej nastapi sortowanie",
                        "Confirmation", MessageBoxButton.OK);
                }
                else
                {
                    ViewModelLocator.SortRecordsCon.Add(SelectedName);
                    if (SortBy=="Rosnaco")
                    {
                        SortBy = "asc";
                    }
                    else if (SortBy=="Malejaco")
                    {
                        SortBy = "desc";
                    }
                    ViewModelLocator.SortRecordsCon.Add(SortBy);
                    ((Window)w).Close();
                }
            });

            OnLoad = new RelayCommand(() =>
            {
               Init();
            });
        }

        /// <summary>
        /// Metoda pobierająca nazwy kolumn i dodaję je do odpowiedniej zmiennej
        /// </summary>
        public void Init()
        {
            TablesName.Clear();
            Table = new DataTable();
            Table = (DataTable)ViewModelLocator.ColumnsName;
            for (int colIndex = 0; colIndex < Table.Columns.Count; colIndex++)
            {
                TablesName.Add(Table.Columns[colIndex].ToString());
            }
            
        }
    }
}
