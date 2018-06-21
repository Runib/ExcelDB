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
    public class SortRecordsViewModel : ViewModelBase
    {
        public ObservableCollection<string> TablesName
        {
            get;
            set;
        }

        public RelayCommand<object> SortCommand { get; set; }
        public RelayCommand OnLoad { get; set; }

        private DataTable table;
        public DataTable Table
        {
            get { return table; }
            set { table = value; RaisePropertyChanged(() => Table); }
        }

        private string sortBy;
        public string SortBy
        {
            get { return sortBy; }
            set { sortBy = value; RaisePropertyChanged(() => SortBy); }
        }

        private string selectedName;
        public string SelectedName
        {
            get { return selectedName; }
            set { selectedName = value; RaisePropertyChanged(() => SelectedName); }
        }

        public SortRecordsViewModel()
        {
            InitCommand();
            TablesName = new ObservableCollection<string>();
        }

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
