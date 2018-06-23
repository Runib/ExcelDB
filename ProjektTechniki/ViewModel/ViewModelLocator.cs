/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:ProjektTechniki"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using NPOI.SS.UserModel;
using ProjektTechniki.Services;
using System;
using System.Collections.Generic;

namespace ProjektTechniki.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Zmienne dostepne w ca³ej aplikacji, pozwalaj¹ one przechowywaæ dane wybrane przez u¿ytkownika jak i sciê¿kê do pliku
        /// </summary>
        public static object Param;
        public static object ColumnsName;
        public static string sheetName;
        public static List<string> SortRecordsCon=new List<string>();
        public static List<CellType> ColumnType = new List<CellType>();
        public static List<string> SearchData = new List<string>();

        /// <summary>
        /// Nazwy widoków dostêpnych w aplikacji przechowywane w stringach, aby móc je dodaæ do odpowiedniej metody
        /// </summary>
        public const string CreateTableKey = "CreateTableView";
        public const string AddRecordKey = "AddRecordView";
        public const string CreateBaseKey = "CreateBaseView";
        public const string LoadBaseKey = "LoadBaseView";
        public const string CreateBaseAddColumnsKey = "CreateBaseAddColumnsView";
        public const string SortRecordsKey = "SortRecordsView";

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SetupNavigation();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<CreateBaseViewModel>();
            SimpleIoc.Default.Register<LoadBaseViewModel>();
            SimpleIoc.Default.Register<ActionLoadedBaseViewModel>();
            SimpleIoc.Default.Register<AddRecordViewModel>();
            SimpleIoc.Default.Register<SortRecordsViewModel>();
            SimpleIoc.Default.Register<SearchRecordViewModel>();
            SimpleIoc.Default.Register<CreateBaseAddColumnsViewModel>();
        }

        /// <summary>
        /// Metoda nawiguj¹ca widokami, pozwala na wyœwietlnei wybranego widoku w danej chwili
        /// </summary>
        private static void SetupNavigation()
        {
            var navigationService = new MyNavigationService();
            navigationService.Configure("CreateBaseView", new Uri("../View/CreateBaseView.xaml", UriKind.Relative));
            navigationService.Configure("LoadBaseView", new Uri("../View/LoadBaseView.xaml", UriKind.Relative));
            navigationService.Configure("CreateBaseAddColumnsView", new Uri("../View/CreateBaseAddColumnsView.xaml", UriKind.Relative));
            SimpleIoc.Default.Register<IMyNavigationService>(() => navigationService);
        }

        /// <summary>
        /// Zmienna przechowuj¹ca nazwê ViewModelu glównego okna
        /// </summary>
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }


        /// <summary>
        /// Zmienna przechowuj¹ca nazwê ViewModelu okna tworzenia bazy/pliku
        /// </summary>
        public CreateBaseViewModel CreateBaseViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CreateBaseViewModel>();
            }
        }

        /// <summary>
        /// Zmienna przechowuj¹ca nazwê ViewModelu okna wyszukiwania
        /// </summary>
        public SearchRecordViewModel SearchRecord
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SearchRecordViewModel>();
            }
        }

        /// <summary>
        /// Zmienna przechowuj¹ca nazwê ViewModelu okna ³adowania pliku/bazy
        /// </summary>
        public LoadBaseViewModel LoadBase
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoadBaseViewModel>();
            }
        }

        /// <summary>
        /// Zmienna przechowuj¹ca nazwê ViewModelu okna wyœwietlaj¹cego zawartoœæ tabel
        /// </summary>
        public ActionLoadedBaseViewModel ActionLoadedBase
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ActionLoadedBaseViewModel>();
            }
        }

        /// <summary>
        /// Zmienna przechowuj¹ca nazwê ViewModelu okna dodawania rekordu
        /// </summary>
        public AddRecordViewModel AddRecord
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddRecordViewModel>();
            }
        }

        /// <summary>
        /// Zmienna przechowuj¹ca nazwê ViewModelu okna sortowania
        /// </summary>
        public SortRecordsViewModel SortRecords
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SortRecordsViewModel>();
            }
        }

        /// <summary>
        /// Zmienna przechowuj¹ca nazwê ViewModelu okna dodawania kolumnd o tabeli
        /// </summary>
        public CreateBaseAddColumnsViewModel AddColumns
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CreateBaseAddColumnsViewModel>();
            }
        }


        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}