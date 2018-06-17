using ExcelLibrary.SpreadSheet;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using ProjektTechniki.Services;
using ProjektTechniki.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;

namespace ProjektTechniki.ViewModel
{
    public class ActionLoadedBaseViewModel : ViewModelBase
    {
        public RelayCommand AddRecordCommand { get; set; }
        public RelayCommand OnLoad{ get; set; }
        public RelayCommand SortRecordsCommand { get; set; }

        List<ISheet> sheets = new List<ISheet>();

        private DataTable table;
        public DataTable Table
        {
            get { return table; }
            set { table = value; RaisePropertyChanged(() => Table); }
        }
        
        public ObservableCollection<string> TablesName
        {
            get;
            set; 
        }

        private string selectedName;
        public string SelectedName
        {
            get { return selectedName; }
            set { selectedName = value; RaisePropertyChanged(() => SelectedName); if(!string.IsNullOrEmpty(value))ChangeTable(); }
        }

        private void ChangeTable()
        {
            ISheet sheet;
            var tempTable = new DataTable();
            var list = new List<object>();
            sheet = sheets.Where(s=>s.SheetName==SelectedName).SingleOrDefault();
            ViewModelLocator.sheetName = sheet.SheetName;
            for (int rowIndex = 0; rowIndex <= sheet.LastRowNum; rowIndex++)
            {
                XSSFRow row = (XSSFRow)sheet.GetRow(rowIndex);
                for (int colIndex = row.FirstCellNum; colIndex < row.LastCellNum; colIndex++)
                {
                    if (rowIndex == sheet.FirstRowNum)
                    {
                        tempTable.Columns.Add((row.GetCell(colIndex).StringCellValue.ToString()));
                    }
                    else
                        list.Add(row.GetCell(colIndex));
                }

                if (list.Count > 0)
                {
                    tempTable.Rows.Add(list.ToArray());
                    list.Clear();
                }
            }
            Table = tempTable;
        }

        public ActionLoadedBaseViewModel()
        {
            TablesName = new ObservableCollection<string>();
            InitCommand();
        }

        private void Init()
        {
            TablesName.Clear();
            sheets = new List<ISheet>();
            string sheetName=null;
            ISheet sheet = null;
            var param = ViewModelLocator.Param as String;
            
            int indexSheet = 0;
            using (var stream = new FileStream(param, FileMode.Open, FileAccess.Read))
            {
                App.workbook = new XSSFWorkbook(stream);
                while (indexSheet<App.workbook.NumberOfSheets) 
                {
                    sheetName = App.workbook.GetSheetAt(indexSheet).SheetName;
                    sheet = (XSSFSheet)(App.workbook.GetSheet(sheetName));
                    if (sheet.GetRow(0) != null)
                    {
                        sheets.Add(sheet);
                        TablesName.Add(sheetName);
                    }
                    indexSheet++;
                    sheet = null;
                    sheetName = null;
                }

                if (TablesName.Count==0)
                {

                }
            }
        }

        private void InitCommand()
        {
            AddRecordCommand = new RelayCommand(() =>
            {
                if (!string.IsNullOrEmpty(SelectedName))
                {
                    ViewModelLocator.ColumnsName = Table;
                    AddRecordView ActionWindow = new AddRecordView();
                    ActionWindow.ShowDialog();
                    Init();
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Brak dostepu",
                            "Confirmation", MessageBoxButton.OK);
                }
                
            });
            OnLoad = new RelayCommand(() => {
                Init();
            });
            SortRecordsCommand = new RelayCommand(() =>
            {
                if (!string.IsNullOrEmpty(SelectedName))
                {
                    ViewModelLocator.ColumnsName = Table;
                    SortRecordsView ActionWindow = new SortRecordsView();
                    ActionWindow.ShowDialog();
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Brak dostepu",
                            "Confirmation", MessageBoxButton.OK);
                }
            });
        }
    }
}
