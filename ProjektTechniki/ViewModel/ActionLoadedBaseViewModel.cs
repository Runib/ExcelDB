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

namespace ProjektTechniki.ViewModel
{
    public class ActionLoadedBaseViewModel : ViewModelBase
    {
        public RelayCommand AddRecordCommand { get; set; }

        private DataTable table;
        public DataTable Table
        {
            get { return table; }
            set { table = value; RaisePropertyChanged(() => Table); }
        }

        public ActionLoadedBaseViewModel()
        {
            Init();
            InitCommand();
        }

        private void Init()
        {
            string sheetName;
            dynamic workbook;
            ISheet sheet = null;
            var param = ViewModelLocator.Param as String;
            var fileExtension = Path.GetExtension(param);
            switch (fileExtension)
            {
                case ".xlsx":
                    using (var stream = new FileStream(param, FileMode.Open, FileAccess.Read))
                    {
                        workbook = new XSSFWorkbook(stream);
                        sheetName = workbook.GetSheetAt(0).SheetName;
                        sheet = (XSSFSheet)workbook.GetSheet(sheetName);
                        break;
                    }
                case ".xls":
                    using (var stream = new FileStream(param, FileMode.Open, FileAccess.Read))
                    {
                        workbook = new XSSFWorkbook(stream);
                        sheetName = workbook.GetSheetAt(0).SheetName;
                        sheet = (XSSFSheet)workbook.GetSheet(sheetName);
                        break;
                    }
            }

            Table = new DataTable();
            var list = new List<object>();

            for (int rowIndex = 0; rowIndex <= sheet.LastRowNum; rowIndex++)
            {
                XSSFRow row = (XSSFRow)sheet.GetRow(rowIndex);
                for (int colIndex = row.FirstCellNum; colIndex < row.LastCellNum; colIndex++)
                {
                    if (rowIndex == sheet.FirstRowNum)
                    {
                        Table.Columns.Add((row.GetCell(colIndex).StringCellValue.ToString()));

                    }
                    else
                        list.Add(row.GetCell(colIndex));
                }

                if (list.Count > 0)
                {
                    Table.Rows.Add(list.ToArray());
                    list.Clear();
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
