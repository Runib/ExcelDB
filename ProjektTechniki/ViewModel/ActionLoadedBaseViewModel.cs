using ExcelLibrary.SpreadSheet;
using GalaSoft.MvvmLight;
<<<<<<< HEAD
using GalaSoft.MvvmLight.Command;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using ProjektTechniki.Services;
using ProjektTechniki.View;
=======
>>>>>>> 147a88c... Addded Load, Create and Displaydupadupacycki
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
<<<<<<< HEAD
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
=======
using System.Linq;
using System.Text;
using System.Threading.Tasks;
>>>>>>> 147a88c... Addded Load, Create and Displaydupadupacycki

namespace ProjektTechniki.ViewModel
{
    public class ActionLoadedBaseViewModel : ViewModelBase
    {
<<<<<<< HEAD
        
        public RelayCommand AddRecordCommand { get; set; }

=======
>>>>>>> 147a88c... Addded Load, Create and Displaydupadupacycki
        private DataTable table;
        public DataTable Table
        {
            get { return table; }
            set { table = value; RaisePropertyChanged(() => Table); }
        }

        public ActionLoadedBaseViewModel()
        {
            Init();
<<<<<<< HEAD
            InitCommand();
=======
>>>>>>> 147a88c... Addded Load, Create and Displaydupadupacycki
        }

        private void Init()
        {
<<<<<<< HEAD
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
=======
            var param = ViewModelLocator.Param as String;
            Workbook book = Workbook.Load(param);
            Worksheet sheet = book.Worksheets[0];
            Table = new DataTable();
            var list = new List<object>();

            for (int rowIndex = sheet.Cells.FirstRowIndex; rowIndex <= sheet.Cells.LastRowIndex; rowIndex++)
            {
                Row row = sheet.Cells.GetRow(rowIndex);
                for (int colIndex = row.FirstColIndex; colIndex <= row.LastColIndex; colIndex++)
                {
                    if (rowIndex == sheet.Cells.FirstRowIndex)
                    {
                        Table.Columns.Add((row.GetCell(colIndex).Value.ToString()));
                    }
                    else
                        list.Add(row.GetCell(colIndex).Value);
>>>>>>> 147a88c... Addded Load, Create and Displaydupadupacycki
                }

                if (list.Count > 0)
                {
                    Table.Rows.Add(list.ToArray());
                    list.Clear();
                }
            }
<<<<<<< HEAD



        }

        private void InitCommand()
        {
            AddRecordCommand = new RelayCommand(() => {
                ViewModelLocator.ColumnsName = Table;
                AddRecordView ActionWindow = new AddRecordView();
                ActionWindow.ShowDialog();
            });
=======
>>>>>>> 147a88c... Addded Load, Create and Displaydupadupacycki
        }
    }



}
