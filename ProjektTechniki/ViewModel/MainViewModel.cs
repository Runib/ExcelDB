using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using ProjektTechniki.Services;
using CommonServiceLocator;
using ProjektTechniki.View;

namespace ProjektTechniki.ViewModel
{
    /// <summary>
    /// Klasa g��wneog okna aplikacji/pierwszego okna aplikacji
    /// umo�liwwia wyb�r stworzenia lub �adowania bazy danych/pliku
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Zmienna nawigacyjna, pozwala nawigowa� pomi�dzy widokami 
        /// </summary>
        private IMyNavigationService navigationService;

        /// <summary>
        /// Deklaracja komendy po��czona z przyciskiem stw�rz baz�, powoduje przej�cie do kolejnego widoku  stworzenia bazy
        /// </summary>
        public RelayCommand CreateBaseCommand { get; set; }

        /// <summary>
        /// Deklaracja komendy po��czona z przyciskiem za�aduj baz�, powoduje przej�cie do kolejnego widoku �adowania bazy
        /// </summary>
        public RelayCommand LoadBaseCommand { get; set; }

        /// <summary>
        /// Deklaracja komendy wywo�ywanej podczas �adowania widoku
        /// </summary>
        public RelayCommand OnLoad { get; set; }

        /// <summary>
        /// Konstruktor klasy, wywo�uje metode InitCommand i ustawia zmienn� nawigacyjn�
        /// </summary>
        /// <param name="navService"></param>
        public MainViewModel(IMyNavigationService navService)
        {
            navigationService = navService;
            InitCommand();
        }

        /// <summary>
        /// Metoda zawieraj�ca cia�a komend zadeklarowanych wczesniej
        /// </summary>
        private void InitCommand()
        {
            OnLoad = new RelayCommand(() =>
            {
               
            });
            CreateBaseCommand = new RelayCommand(() => { navigationService.NavigateTo(ViewModelLocator.CreateBaseKey); });
            LoadBaseCommand = new RelayCommand(() => { navigationService.NavigateTo(ViewModelLocator.LoadBaseKey); });
        }
    }
}