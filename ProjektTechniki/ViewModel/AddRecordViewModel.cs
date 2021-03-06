﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using NPOI.SS.UserModel;
using ProjektTechniki.Services;
using ProjektTechniki.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ProjektTechniki.ViewModel
{
    /// <summary>
    /// Klasa widoku AddRecordViewModel
    /// Połączona z widokiem, pobiera od niego wypełnione wiersze a nastepnie dodaje do pliku
    /// Nazwy kolumn pobiera ze zmiennej zawierającej nazwykolumn i dostępnej z klasy ViewModelLocator
    /// </summary>
    public class AddRecordViewModel : ViewModelBase
    {
        /// <summary>
        /// Zmienna zawierająca wszystkie wiersze które należy dodać do pliku
        /// </summary>
        private List<string[]> RowsList = new List<string[]>();

        /// <summary>
        /// Deklaracja komendy wywoływanej podczas wciśnięcia przycisku Dodaj, dodaje do pliku odpowiednie wiersze uwzględniając typ i wychodiz z okna
        /// </summary>
        public RelayCommand<object> AddRecordCommand { get; set; }

        /// <summary>
        /// Zmienna wywolywana podczas każdej zmiany wartości komórki w oknie wykonanej przez użytkownika
        /// </summary>
        public RelayCommand<object> RowEditEndingCommand { get; set; }

        /// <summary>
        /// Deklaracja zmiennej służącej do odpowiednie wczytania nazw kolumn
        /// </summary>
        private DataTable table;

        /// <summary>
        /// Deklaracja zmiennej służącej do odpowiednie wczytania nazw kolumn
        /// </summary>
        public DataTable Table
        {
            get { return table; }
            set { table = value; RaisePropertyChanged(() => Table); }
        }

        /// <summary>
        /// Deklaracja zmiennej przechowującej nazwy kolumn
        /// </summary>
        private DataTable columnsName;

        /// <summary>
        /// Deklaracja zmiennej przechowującej nazwy kolumn
        /// </summary>
        public DataTable ColumnsName
        {
            get { return columnsName; }
            set { columnsName = value; RaisePropertyChanged(() => ColumnsName); }
        }

        /// <summary>
        /// Deklaracja zmiennej pomocnicza, pomaga przy dodawaniu wierszy do pliku
        /// </summary>
        private DataRowView selectedItem;

        /// <summary>
        /// Deklaracja zmiennej pomocnicza, pomaga przy dodawaniu wierszy do pliku
        /// </summary>
        public DataRowView SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; RaisePropertyChanged(() => SelectedItem); }
        }

       
        /// <summary>
        /// Konstruktor klasy wywołujący dwie metody, Init oraz InitCommand
        /// </summary>
        public AddRecordViewModel()
        {
            Init();
            InitCommand();
        }

        /// <summary>
        /// Metoda obsługująca wszystkie komendy połączone z przyciskami 
        /// Dodaje wiersze niepuste do pliku uwzgledniajac ich typ
        /// Uwzglednia kazda zmiane wartości komórek w wyświetlanej tabeli
        /// Sprawdza wyjątki kiedy typ się nie zgadza 
        /// </summary>
        private void InitCommand()
        {
            RowEditEndingCommand = new RelayCommand<object>(e =>
           {
               var args = (DataGridCellEditEndingEventArgs)e;
               var row = args.Row;
               var index = row.GetIndex();
               var item = ((TextBox)args.EditingElement).Text;

               if (index > RowsList.Count - 1 && RowsList!=null && item != null)
               {
                   var arr = new string[ColumnsName.Columns.Count];
                   arr[args.Column.DisplayIndex] = item;
                   RowsList.Add(arr);
               }
               else if(RowsList!=null && item!=null)
                   RowsList[index][args.Column.DisplayIndex] = item;

           });
           AddRecordCommand = new RelayCommand<object>(w =>
           {
               ISheet sheet;
               List<string[]> helpList = new List<string[]>();
               bool canAdd;
               sheet = App.workbook.GetSheet(ViewModelLocator.sheetName);
               for (int i = 0; i < RowsList.Count; i++)
               {
                   canAdd = true;
                   for (int j = 0; j < RowsList[i].Length; j++)
                   {

                       if (RowsList[i][j] == null || RowsList[i][j] == "")
                       {
                           canAdd = false;
                           break;
                       }

                       if (sheet.GetRow(1) != null )
                       {
                           var cellType = sheet.GetRow(1).GetCell(j).CellType;

                           switch (cellType)
                           {
                               case CellType.Numeric:
                                   double result;

                                   if (!(double.TryParse(RowsList[i][j], out result)))
                                   {
                                       MessageBox.Show($"Niepoprawny typ w wierszu {i + 1} i kolumnie {j + 1}" +
                                           $"\nWymagany typ to {cellType}",
                                       "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                                       return;
                                   }
                                   break;
                               case CellType.Boolean:
                                   bool resultBool;
                                   if (!(bool.TryParse(RowsList[i][j], out resultBool)))
                                   {
                                       MessageBox.Show($"Niepoprawny typ w wierszu {i + 1} i kolumnie {j + 1}" +
                                           $"\nWymagany typ to {cellType}",
                                       "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                                       return;
                                   }
                                   break;
                               default:
                                   break;
                           }
                       }
                       else
                       {

                           var cellType = ViewModelLocator.ColumnType[j];

                           switch (cellType)
                           {
                               case CellType.Numeric:
                                   double result;

                                   if (!(double.TryParse(RowsList[i][j], out result)))
                                   {
                                       MessageBox.Show($"Niepoprawny typ w wierszu {i + 1} i kolumnie {j + 1}" +
                                           $"\nWymagany typ to {cellType}",
                                       "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                                       helpList.Clear();
                                       return;
                                   }
                                   break;
                               case CellType.Boolean:
                                   bool resultBool;
                                   if (!(bool.TryParse(RowsList[i][j], out resultBool)))
                                   {
                                       MessageBox.Show($"Niepoprawny typ w wierszu {i + 1} i kolumnie {j + 1}" +
                                           $"\nWymagany typ to {cellType}",
                                       "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                                       helpList.Clear();
                                       return;
                                   }
                                   break;
                               default:
                                   break;
                           }
                       }
                       
                   }
                   if (canAdd)
                       helpList.Add(RowsList[i]);
               }
                   if (helpList.Count > 0)
                   {
                       for (int i = 0; i < helpList.Count; i++)
                       {
                           IRow row = sheet.CreateRow(sheet.LastRowNum + 1);
                           for (int j = 0; j < helpList[i].Length; j++)
                           {
                               CellType cellType;
                               if (sheet.GetRow(2) != null)
                               {
                                   cellType = sheet.GetRow(1).GetCell(j).CellType;
                               }
                               else
                               {
                                   double result;
                                   bool resultBool;
                                   if (double.TryParse(RowsList[i][j], out result))
                                   {
                                       cellType = CellType.Numeric;
                                   }
                                   else if (bool.TryParse(RowsList[i][j], out resultBool))
                                   {
                                       cellType = CellType.Boolean;
                                   }
                                   else
                                   {
                                       cellType = CellType.String;
                                   }
                               }

                               if (cellType == CellType.Numeric)
                               {
                                   cellType = CellType.Numeric;
                                   var cell = row.CreateCell(j, cellType);
                                   cell.SetCellValue(Convert.ToDouble(helpList[i][j]));
                               }
                               else if (cellType == CellType.Boolean)
                               {
                                   cellType = CellType.Boolean;
                                   var cell = row.CreateCell(j, cellType);
                                   cell.SetCellValue(Convert.ToBoolean(helpList[i][j]));
                               }
                               else
                               {
                                   cellType = CellType.String;
                                   var cell = row.CreateCell(j, cellType);
                                   cell.SetCellValue(Convert.ToString(helpList[i][j]));
                               }
                           }
                       }
                       RowsList.Clear();
                       Init();
                       var param = ViewModelLocator.Param as String;
                       var stream = new FileStream(param, FileMode.Create, FileAccess.Write);
                       App.workbook.Write(stream);
                       stream.Close();
                       if (helpList.Count < RowsList.Count)
                       {
                           MessageBox.Show("Wypełnij cały wiersz.",
                               "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                       }
                        ((Window)w).Close();

                   }
                   else
                   {
                       MessageBoxResult result = MessageBox.Show("Wypełnij cały wiersz.",
                               "Confirmation", MessageBoxButton.OK);
                   }
               }
           );
        }

        /// <summary>
        /// Metoda której zadaniem jest pobranie nazw kolumn i poprzez połączenie przekazanie ich do widoku
        /// </summary>
        private void Init()
        {
            Table = new DataTable();
            Table = (DataTable)ViewModelLocator.ColumnsName;
            ColumnsName = new DataTable();
            for (int colIndex = 1; colIndex < Table.Columns.Count; colIndex++)
            {
                ColumnsName.Columns.Add(Table.Columns[colIndex].ToString());
            }
            ColumnsName.Rows.Add();
        }
    }
}
