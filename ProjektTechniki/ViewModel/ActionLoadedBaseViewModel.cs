using ExcelLibrary.SpreadSheet;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektTechniki.ViewModel
{
    public class ActionLoadedBaseViewModel : ViewModelBase
    {
        private DataTable table;
        public DataTable Table
        {
            get { return table; }
            set { table = value; RaisePropertyChanged(() => Table); }
        }

        public ActionLoadedBaseViewModel()
        {
            Init();
        }

        private void Init()
        {
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
                }

                if (list.Count > 0)
                {
                    Table.Rows.Add(list.ToArray());
                    list.Clear();
                }
            }
        }
    }



}
