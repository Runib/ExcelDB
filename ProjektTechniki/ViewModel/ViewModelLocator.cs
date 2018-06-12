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
using ProjektTechniki.Services;
using System;

namespace ProjektTechniki.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        public static object Param;
<<<<<<< HEAD
        public static object ColumnsName;

=======
        
>>>>>>> 147a88c... Addded Load, Create and Displaydupadupacycki
        public const string CreateTableKey = "CreateTableView";
        public const string AddRecordKey = "AddRecordView";
        public const string CreateBaseKey = "CreateBaseView";
        public const string LoadBaseKey = "LoadBaseView";
<<<<<<< HEAD
        public const string CreateBaseAddColumnsKey = "CreateBaseAddColumnsView";
=======
>>>>>>> 147a88c... Addded Load, Create and Displaydupadupacycki
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
<<<<<<< HEAD
            SimpleIoc.Default.Register<AddRecordViewModel>();
            SimpleIoc.Default.Register<CreateBaseAddColumnsViewModel>();
=======
>>>>>>> 147a88c... Addded Load, Create and Displaydupadupacycki


        }

        private static void SetupNavigation()
        {
            var navigationService = new MyNavigationService();
            navigationService.Configure("CreateBaseView", new Uri("../View/CreateBaseView.xaml", UriKind.Relative));
            navigationService.Configure("LoadBaseView", new Uri("../View/LoadBaseView.xaml", UriKind.Relative));
<<<<<<< HEAD
            navigationService.Configure("CreateBaseAddColumnsView", new Uri("../View/CreateBaseAddColumnsView.xaml", UriKind.Relative));
=======
>>>>>>> 147a88c... Addded Load, Create and Displaydupadupacycki
            SimpleIoc.Default.Register<IMyNavigationService>(() => navigationService);
            // ten kontener IoC moze przechowywac rozne serwisy, do ktorych sie mozesz odwolywac w ViewModelach.
            //oo no dobra wazne info
        }


        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        

        public CreateBaseViewModel CreateBaseViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CreateBaseViewModel>();
            }
        }

        public LoadBaseViewModel LoadBase
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoadBaseViewModel>();
            }
        }

        public ActionLoadedBaseViewModel ActionLoadedBase
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ActionLoadedBaseViewModel>();
            }
        }
<<<<<<< HEAD


        public AddRecordViewModel AddRecord
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddRecordViewModel>();
            }
        }

        public CreateBaseAddColumnsViewModel AddColumns
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CreateBaseAddColumnsViewModel>();
            }
        }

=======
        
>>>>>>> 147a88c... Addded Load, Create and Displaydupadupacycki
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}