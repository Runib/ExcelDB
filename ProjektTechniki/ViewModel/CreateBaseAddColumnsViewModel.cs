﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using ProjektTechniki.Services;
using ProjektTechniki.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjektTechniki.ViewModel
{
    public class CreateBaseAddColumnsViewModel :ViewModelBase
    {
        private IMyNavigationService navigationService;

        public RelayCommand AddColumnCommand { get; set; }
        public RelayCommand GoNextCommand { get; set; }

        private string addOrNot;
        public string AddOrNot
        {
            get { return addOrNot; }
            set { addOrNot = value; RaisePropertyChanged(() => AddOrNot); }
        }

        private string columnName;
        public string ColumnName
        {
            get { return columnName; }
            set { columnName = value; RaisePropertyChanged(() => ColumnName); }
        }

        private DataTable dt;
        public DataTable Dt
        {
            get { return dt; }
            set { dt = value; RaisePropertyChanged(() => Dt); }
        }


        public CreateBaseAddColumnsViewModel(IMyNavigationService navService)
        {
            this.navigationService = navService;
            Dt = new DataTable();
            InitCommand();
        }
        // jest git, bo nic nie wywala, teraz fun, no ok, baw sie dobrze dalej XD
        //te serwisy, w sumie to jeden serwis, on jest wstrzykiwany w konstruktorze przez cos takiego jak IoC, i to jest tu...

        private void InitCommand()
        {
            AddColumnCommand = new RelayCommand(()=>
            {
                if (ColumnName == null || ColumnName.FirstOrDefault() >= '0' && ColumnName.FirstOrDefault() <= '9')
                {
                    MessageBoxResult result = MessageBox.Show("Podaj prawidłową nazwę",
                        "Confirmation", MessageBoxButton.OK);
                    AddOrNot = "";
                }
                else
                {
                    Dt.Columns.Add(ColumnName);
                    ColumnName = "";
                    AddOrNot = "Dodano";
                }
            });

            GoNextCommand = new RelayCommand(() =>
            {
                string path = navigationService.Parameter.ToString();
                var fileExtension = Path.GetExtension(path);
                FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
                IWorkbook woorkbook = new XSSFWorkbook(stream);
                stream.Close();
                string SheetName = woorkbook.GetSheetName(0);
                ISheet sheet = woorkbook.GetSheet(SheetName);
                IRow row = sheet.CreateRow(0);
                
                
                for (int j = 0; j <Dt.Columns.Count; j++)
                {
                    var cell = row.CreateCell(j);
                    cell.SetCellValue(Dt.Columns[j].ToString());
                    sheet.AutoSizeColumn(j);
                }


                stream = new FileStream(path, FileMode.Create, FileAccess.Write);
                woorkbook.Write(stream);

                stream.Close();

                AddOrNot = "";
                Dt.Clear();
                App.Current.MainWindow.Hide();
                ViewModelLocator.Param = path;
                ActionLoadedBase ActionWindow = new ActionLoadedBase();
                ActionWindow.ShowDialog();

                App.Current.MainWindow.Show();
            });
        }
    }
}