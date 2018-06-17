using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using NPOI.SS.UserModel;
using ProjektTechniki.Services;
using ProjektTechniki.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ProjektTechniki.ViewModel
{
    public class AddRecordViewModel : ViewModelBase
    {
        private List<string[]> RowsList = new List<string[]>();

        public RelayCommand<object> AddRecordCommand { get; set; }
        public RelayCommand<object> RowEditEndingCommand { get; set; }

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

               if (index > RowsList.Count - 1 && RowsList!=null)
               {
                   var arr = new string[ColumnsName.Columns.Count];
                   arr[args.Column.DisplayIndex] = item;
                   RowsList.Add(arr);
               }
               else if(RowsList!=null)
                   RowsList[index][args.Column.DisplayIndex] = item;

           });

            AddRecordCommand = new RelayCommand<object>(w => {
                ISheet sheet;
                int AddOrNot = 1;
                sheet=App.workbook.GetSheet(ViewModelLocator.sheetName);
                for (int i = 0; i < RowsList.Count; i++)
                {
                    for (int j = 0; j < RowsList[i].Length; j++)
                    {
                        if(RowsList[i][j]==null || RowsList[i][j]=="")
                        {
                            AddOrNot = 0;
                            break;
                        }
                    }
                }
                if (AddOrNot == 1)
                {
                    for (int i = 0; i < RowsList.Count; i++)
                    {
                        IRow row = sheet.CreateRow(sheet.LastRowNum +1);
                        for (int j = 0; j < RowsList[i].Length; j++)
                        {
                            var cell = row.CreateCell(j);
                            cell.SetCellValue(RowsList[i][j]);
                        }
                    }
                    RowsList.Clear();
                    Init();
                    var param = ViewModelLocator.Param as String;
                    var stream = new FileStream(param, FileMode.Open, FileAccess.ReadWrite);
                    App.workbook.Write(stream);
                    stream.Close();
                    ((Window)w).Close();
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Wypełnij cały wiersz.",
                            "Confirmation", MessageBoxButton.OK);
                }
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
