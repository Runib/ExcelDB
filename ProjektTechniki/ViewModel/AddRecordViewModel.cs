using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using ProjektTechniki.Services;
using ProjektTechniki.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace ProjektTechniki.ViewModel
{
    public class AddRecordViewModel : ViewModelBase
    {
        private List<string[]> RowsList = new List<string[]>();

        public RelayCommand AddRecordCommand { get; set; }
        private DataTable table;
        public DataTable Table
        {
            get { return table; }
            set { table = value; RaisePropertyChanged(() => Table); }
        }

        private DataTable columnsName;
        public DataTable ColumnsName
        {
            get { return columnsName; }
            set { columnsName = value; RaisePropertyChanged(() => ColumnsName); }
        }

        private DataRowView selectedItem;
        public DataRowView SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; RaisePropertyChanged(() => SelectedItem); }
        }

        public RelayCommand<object> RowEditEndingCommand { get; set; }

        public AddRecordViewModel()
        {
            Init();
            InitCommand();
        }

        private void InitCommand()
        {
            RowEditEndingCommand = new RelayCommand<object>(e =>
           {
               var args = (DataGridCellEditEndingEventArgs)e;
               var row = args.Row;
               var index = row.GetIndex();
               var item = ((TextBox)args.EditingElement).Text;

               if (index > RowsList.Count - 1)
               {
                   var arr = new string[ColumnsName.Columns.Count];
                   arr[args.Column.DisplayIndex] = item;
                   RowsList.Add(arr);
               }
               else
                   RowsList[index][args.Column.DisplayIndex] = item;
           });

            AddRecordCommand = new RelayCommand(() => {
                
            });
        }

        private void Init()
        {
            Table = new DataTable();
            Table = (DataTable)ViewModelLocator.ColumnsName;
            ColumnsName = new DataTable();
            for (int colIndex = 0; colIndex < Table.Columns.Count; colIndex++)
            {
                ColumnsName.Columns.Add(Table.Columns[colIndex].ToString());
            }
            ColumnsName.Rows.Add();
        }
    }
}
