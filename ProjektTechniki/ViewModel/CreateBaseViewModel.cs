using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.CommandWpf;
using ProjektTechniki.Services;
using System.Data;
using ExcelLibrary.SpreadSheet;
using GalaSoft.MvvmLight;

namespace ProjektTechniki.ViewModel
{
    public class CreateBaseViewModel : ViewModelBase
    {
        private IMyNavigationService navigationService;
        DataSet ds = new DataSet("New_DataSet");
        public RelayCommand CreateBaseCommand { get; set; }

        private string baseName;
        public string BaseName
        {
            get { return baseName; }
            set { baseName = value; RaisePropertyChanged(() => BaseName); }
        }

        public CreateBaseViewModel(IMyNavigationService navService)
        {
            navigationService = navService;
            InitCommand();
        }

        private void InitCommand()
        {
            CreateBaseCommand = new RelayCommand(() =>
            {
                string file = $"D:\\{BaseName}.xls";
                Workbook workbook = new Workbook();
                Worksheet worksheet = new Worksheet("First Sheet");
                worksheet.Cells[0, 1] = new Cell((short)1);
                worksheet.Cells[2, 0] = new Cell(9999999);
                worksheet.Cells[3, 3] = new Cell((decimal)3.45);
                worksheet.Cells[2, 2] = new Cell("Text string");
                worksheet.Cells[2, 4] = new Cell("Second string");
                worksheet.Cells[4, 0] = new Cell(32764.5, "#,##0.00");
                worksheet.Cells[5, 1] = new Cell(DateTime.Now, @"YYYY-MM-DD");
                worksheet.Cells.ColumnWidth[0, 1] = 3000;
                workbook.Worksheets.Add(worksheet);
                workbook.Save(file);


            });
        }


    }
}
