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
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;
using System.Windows;

namespace ProjektTechniki.ViewModel
{
    public class CreateBaseViewModel : ViewModelBase
    {
        private IMyNavigationService navigationService;
        public RelayCommand CreateBaseCommand { get; set; }

        private string baseName;
        public string BaseName
        {
            get { return baseName; }
            set { baseName = value; RaisePropertyChanged(() => BaseName); }
        }

        private string tableName;
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; RaisePropertyChanged(() => TableName); }
        }

        private string pathName;
        public string PathName
        {
            get { return pathName; }
            set { pathName = value; RaisePropertyChanged(() => PathName); }
        }

        public CreateBaseViewModel DataContext { get; private set; }

        public CreateBaseViewModel(IMyNavigationService navService)
        {
            navigationService = navService;
            InitCommand();
        }

        private void InitCommand()
        {
            CreateBaseCommand = new RelayCommand(() =>
            {
                if (PathName == null)
                {
                    MessageBoxResult result = MessageBox.Show("Wybierz odpowiednie rozszerzenie",
                        "Confirmation", MessageBoxButton.OK);
                }
                else if (BaseName == null || BaseName == "")
                {
                    MessageBoxResult result = MessageBox.Show("Podaj nazwę bazy",
                        "Confirmation", MessageBoxButton.OK);
                }
                else if (TableName == null || TableName == "")
                {
                    MessageBoxResult result = MessageBox.Show("Podaj nazwę arkusza",
                        "Confirmation", MessageBoxButton.OK);
                }
                else if (BaseName.FirstOrDefault() >= '0' && BaseName.FirstOrDefault() <= '9')
                {
                    MessageBoxResult result = MessageBox.Show("Nazwa nie powinna zaczynać się cyfrą\n" +
                        "lub być liczbą",
                        "Confirmation", MessageBoxButton.OK);
                    BaseName = "";
                }
                else if (TableName.FirstOrDefault() >= '0' && TableName.FirstOrDefault() <= '9')
                {
                    MessageBoxResult result = MessageBox.Show("Nazwa nie powinna zaczynać się cyfrą\n" +
                        "lub być liczbą",
                        "Confirmation", MessageBoxButton.OK);
                    TableName = "";
                }
                else
                {
                    string file = $"D:\\{BaseName}.{PathName}";
                    var stream = new FileStream(file, FileMode.Create, FileAccess.Write);
                    //XSSFWorkbook workbook = new XSSFWorkbook();
                    XSSFSheet sheet = (XSSFSheet)(App.workbook.CreateSheet($"{TableName}"));
                    App.workbook.Write(stream);
                    stream.Close();
                    navigationService.NavigateTo(ViewModelLocator.CreateBaseAddColumnsKey, file);
                }
            });
        }


    }
}
