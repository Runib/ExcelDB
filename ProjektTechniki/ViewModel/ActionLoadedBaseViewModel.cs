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
        private DataTable originalTable;
        private int numberOfSearch=1;

        public RelayCommand AddRecordCommand { get; set; }
        public RelayCommand OnLoad { get; set; }
        public RelayCommand OnUnload { get; set; }
        public RelayCommand SortRecordsCommand { get; set; }
        public RelayCommand DeleteRecordCommand { get; set; }
        public RelayCommand SearchRecordCommand { get; set; }
        public RelayCommand ResetCommand { get; set; }

        List<ISheet> sheets = new List<ISheet>();

        private DataTable table;
        public DataTable Table
        {
            get { return table; }
            set { table = value; RaisePropertyChanged(() => Table); }
        }


        private DataRowView selectedRow;
        public DataRowView SelectedRow
        {
            get { return selectedRow; }
            set { selectedRow = value; RaisePropertyChanged(() => SelectedRow); }
        }

        public ObservableCollection<string> TablesName
        {
            get;
            set;
        }

        private string selectedName;
        private string path;

        public string SelectedName
        {
            get { return selectedName; }
            set { selectedName = value; RaisePropertyChanged(() => SelectedName); if (!string.IsNullOrEmpty(value)) ChangeTable(); }
        }

        private double operationTime;
        public double OperationTime
        {
            get { return operationTime; }
            set { operationTime = value; RaisePropertyChanged(() => OperationTime); }
        }

        private void ChangeTable()
        {
            var startTime = DateTime.Now;

            ISheet sheet;
            originalTable = new DataTable();
            var list = new List<object>();
            sheet = sheets.Where(s => s.SheetName == SelectedName).SingleOrDefault();
            ViewModelLocator.sheetName = sheet.SheetName;

            for (int rowIndex = 0; rowIndex <= sheet.LastRowNum; rowIndex++)
            {
                DataRow NewRow = null;
                IRow row = sheet.GetRow(rowIndex);
                IRow row2 = null;
                IRow row3 = null;

                if (rowIndex == 0)
                {
                    row2 = sheet.GetRow(rowIndex + 1);
                    row3 = sheet.GetRow(rowIndex + 2);
                }
                if (row != null)
                {
                    int colIndex = 0;

                    foreach (ICell cell in row.Cells)
                    {
                        if (rowIndex > 0 && cell.ColumnIndex + 1 >= originalTable.Columns.Count)
                        {
                            break;
                        }

                        if (rowIndex > 0 && NewRow == null)
                        {
                            NewRow = originalTable.NewRow();
                            NewRow[0] = rowIndex;
                        }

                        dynamic valorCell;
                        string cellType = "";
                        string[] cellType2 = new string[2];
                        if (rowIndex == 0)
                        {
                            for (int i = 0; i < 2; i++)
                            {
                                ICell cell2 = null;
                                if (i == 0 && row2 != null) { cell2 = row2.GetCell(cell.ColumnIndex); }
                                else if (row3 != null) { cell2 = row3.GetCell(cell.ColumnIndex); }

                                if (cell2 != null)
                                {
                                    switch (cell2.CellType)
                                    {
                                        case CellType.Blank: break;
                                        case CellType.Boolean: cellType2[i] = "System.String"; break;
                                        case CellType.String: cellType2[i] = "System.String"; break;
                                        case CellType.Numeric:
                                            if (HSSFDateUtil.IsCellDateFormatted(cell2)) { cellType2[i] = "System.DateTime"; }
                                            else
                                            {
                                                cellType2[i] = "System.Double";
                                            }
                                            break;
                                        default:
                                            cellType2[i] = "System.String"; break;
                                    }
                                }
                            }

                            if (cellType2[0] == cellType2[1])
                            {
                                cellType = cellType2[0];
                            }
                            else
                            {
                                if (cellType2[0] == null) cellType = cellType2[1];
                                if (cellType2[1] == null) cellType = cellType2[0];
                                if (cellType == "") cellType = "System.String";
                            }

                            if (cellType == null)
                            {
                                cellType = "System.String";
                            }
                            string colName = "Column_{0}";
                            colName = cell.StringCellValue;
                            //colName = string.Format(colName, colIndex);

                            if (colIndex == 0)
                            {
                                DataColumn ColumnsName1 = new DataColumn("Id", typeof(int));
                                originalTable.Columns.Add(ColumnsName1);
                            }
                            foreach (DataColumn col in originalTable.Columns)
                            {
                                if (col.ColumnName == colName) colName = string.Format("{0}_{1}", colName, colIndex);
                            }
                            DataColumn ColumnsName = new DataColumn(colName, System.Type.GetType(cellType));
                            originalTable.Columns.Add(ColumnsName); colIndex++;
                        }
                        else
                        {
                            switch (cell.CellType)
                            {

                                case CellType.Boolean:
                                    valorCell = cell.BooleanCellValue ? "True" : "False";
                                    break;
                                case CellType.String: valorCell = cell.StringCellValue; break;
                                case CellType.Numeric:
                                    if (HSSFDateUtil.IsCellDateFormatted(cell)) { valorCell = cell.DateCellValue; }
                                    else { valorCell = cell.NumericCellValue; }
                                    break;
                                default:
                                    valorCell = cell.StringCellValue;

                                    if (string.IsNullOrEmpty(valorCell.ToString()))
                                        valorCell = " ";

                                    break;
                            }

                            if (cell.ColumnIndex < originalTable.Columns.Count) NewRow[cell.ColumnIndex + 1] = valorCell;
                        }
                    }
                }

                if (rowIndex > 0 && NewRow != null) originalTable.Rows.Add(NewRow);

                NewRow = null;
            }
            Table = originalTable.Copy();
            Table.AcceptChanges();

            OperationTime = DateTime.Now.Subtract(startTime).TotalSeconds;
        }

        public ActionLoadedBaseViewModel()
        {
            TablesName = new ObservableCollection<string>();
            InitCommand();
        }

        private void Init()
        {
            var startTime = DateTime.Now;

            TablesName.Clear();
            sheets = new List<ISheet>();
            string sheetName = null;
            ISheet sheet = null;
            path = ViewModelLocator.Param as String;

            int indexSheet = 0;
            try
            {
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    App.workbook = new XSSFWorkbook(stream);
                    if (App.workbook.NumberOfSheets == 0)
                    {
                        MessageBox.Show("Nie można załadować pliku bazy.", "Błąd wczytywania", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    while (indexSheet < App.workbook.NumberOfSheets)
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
                }
            }
            catch
            {
                MessageBox.Show("Nie można załadować pliku bazy.", "Błąd wczytywania", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            OperationTime = DateTime.Now.Subtract(startTime).TotalSeconds;
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
            OnLoad = new RelayCommand(() =>
            {
                Init();
            });

            OnUnload = new RelayCommand(() =>
            {
                Table = null;
            });

            SortRecordsCommand = new RelayCommand(() =>
            {
                if (!string.IsNullOrEmpty(SelectedName))
                {
                    ViewModelLocator.ColumnsName = Table;
                    SortRecordsView ActionWindow = new SortRecordsView();
                    ActionWindow.ShowDialog();
                    if (!(ViewModelLocator.SortRecordsCon.Count <1))
                    {
                        var startTime = DateTime.Now;
                        DataView dataView = Table.DefaultView;
                        if (ViewModelLocator.SortRecordsCon.Count == 2)
                        {
                            dataView.Sort = $"{ViewModelLocator.SortRecordsCon[0]} {ViewModelLocator.SortRecordsCon[1]}";
                            Table = dataView.ToTable();
                        }
                        ViewModelLocator.SortRecordsCon.Clear();
                        OperationTime = DateTime.Now.Subtract(startTime).TotalSeconds;
                    }
                    
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Brak dostepu",
                            "Confirmation", MessageBoxButton.OK);
                }
            });
            DeleteRecordCommand = new RelayCommand(() =>
            {
                if (!string.IsNullOrEmpty(SelectedName) && SelectedRow != null)
                {
                    var sheet = sheets.Where(s => s.SheetName == SelectedName).Single();
                    var row = sheet.GetRow(int.Parse(SelectedRow.Row.ItemArray[0].ToString()));
                    sheet.RemoveRow(row);

                    if (row.RowNum != sheet.LastRowNum)
                        sheet.ShiftRows(row.RowNum + 1, sheet.LastRowNum, -1);

                    using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write))
                    {
                        App.workbook.Write(stream);
                    }

                    ChangeTable();
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Brak dostepu",
                            "Confirmation", MessageBoxButton.OK);
                }
            });

            SearchRecordCommand = new RelayCommand(() =>
            {
                if (!string.IsNullOrEmpty(SelectedName))
                {

                    var helperTable = new DataTable();
                    if (numberOfSearch == 1)
                        helperTable = originalTable.Copy();
                    else
                        helperTable = helperTable.Copy();
                    helperTable.Rows.Clear();

                    ViewModelLocator.ColumnsName = Table;

                    SearchRecordView ActionWindow = new SearchRecordView();
                    ActionWindow.ShowDialog();
                    if (!(ViewModelLocator.SearchData.Count<1))
                    {
                        var startTime = DateTime.Now;
                        string searchText = ViewModelLocator.SearchData[0];
                        string searchcolumn = ViewModelLocator.SearchData[1];

                        ViewModelLocator.SearchData.Clear();

                        var columnIndex = originalTable.Columns.IndexOf(searchcolumn);

                        for (int intRow = 0; intRow < originalTable.Rows.Count; intRow++)
                        {
                            var row = originalTable.Rows[intRow];

                            var cell = row.ItemArray[columnIndex];

                            if (cell.ToString().ToUpper().Contains(searchText.ToUpper()))
                                helperTable.Rows.Add(row.ItemArray);
                        }

                        Table = helperTable.Copy();
                        OperationTime = DateTime.Now.Subtract(startTime).TotalSeconds;
                    }
                   
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Brak dostepu",
                            "Confirmation", MessageBoxButton.OK);
                }
            });

            ResetCommand = new RelayCommand(() =>
            {
                numberOfSearch = 1;
                Table = null;
                Init();
            });
        }
    }
}
