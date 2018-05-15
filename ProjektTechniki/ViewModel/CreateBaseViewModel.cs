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
                workbook.Worksheets.Add(worksheet);
                workbook.Save(file);
            });
        }


    }
}
