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
using System.Data;
using System.IO;
using System.Linq;

namespace ProjektTechniki.ViewModel
{
    public class ActionLoadedBaseViewModel : ViewModelBase
    {
        public RelayCommand AddRecordCommand { get; set; }

        List<ISheet> sheets = new List<ISheet>();

        private DataTable table;
        public DataTable Table
        {
            get { return table; }
            set { table = value; RaisePropertyChanged(() => Table); }
        }
        private List<string> tablesName;
        public List<string> TablesName
        {
            get { return tablesName; }
            set { tablesName = value; RaisePropertyChanged(() => TablesName); }
        }

        private string selectedName;
        public string SelectedName
        {
            get { return selectedName; }
            set { selectedName = value; RaisePropertyChanged(() => SelectedName); ChangeTable(); }
        }

        private void ChangeTable()
        {
            ISheet sheet;
            var tempTable = new DataTable();
            var list = new List<object>();
            sheet = sheets.Where(s=>s.SheetName==SelectedName).SingleOrDefault();
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

            Init();
            InitCommand();
        }

        private void Init()
        {
            IWorkbook workbook = new XSSFWorkbook();
            string sheetName=null;
            ISheet sheet = null;
            TablesName = new List<string>();
            var param = ViewModelLocator.Param as String;
            
            int indexSheet = 0;
            using (var stream = new FileStream(param, FileMode.Open, FileAccess.Read))
            {
                workbook = new XSSFWorkbook(stream);
                while (indexSheet<workbook.NumberOfSheets) 
                {
                    sheetName = workbook.GetSheetAt(indexSheet).SheetName;
                    sheet = (XSSFSheet)workbook.GetSheet(sheetName);
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

        private void InitCommand()
        {
            AddRecordCommand = new RelayCommand(() =>
            {
                ViewModelLocator.ColumnsName = Table;
                AddRecordView ActionWindow = new AddRecordView();
                ActionWindow.ShowDialog();
            });
        }
    }
}
