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
    public class SearchRecordViewModel : ViewModelBase
    {
        public RelayCommand<object> SearchCommand { get; set; }
        public RelayCommand OnLoad { get; set; }



        public ObservableCollection<string> TablesName
        {
            get;
            set;
        }

        private string selectedName;
        public string SelectedName
        {
            get { return selectedName; }
            set { selectedName = value; RaisePropertyChanged(() => SelectedName); }
        }

        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set { searchText = value; RaisePropertyChanged(() => SearchText); }
        }

        private DataTable table;
        public DataTable Table
        {
            get { return table; }
            set { table = value; RaisePropertyChanged(() => Table); }
        }

        public SearchRecordViewModel()
        {
            TablesName = new ObservableCollection<string>();
            InitCommand();
        }

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
