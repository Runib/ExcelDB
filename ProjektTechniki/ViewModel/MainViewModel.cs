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
    /// Klasa g³ówneog okna aplikacji/pierwszego okna aplikacji
    /// umo¿liwwia wybór stworzenia lub ³adowania bazy danych/pliku
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Zmienna nawigacyjna, pozwala nawigowaæ pomiêdzy widokami 
        /// </summary>
        private IMyNavigationService navigationService;

        /// <summary>
        /// Deklaracja komendy po³¹czona z przyciskiem stwórz bazê, powoduje przejœcie do kolejnego widoku  stworzenia bazy
        /// </summary>
        public RelayCommand CreateBaseCommand { get; set; }

        /// <summary>
        /// Deklaracja komendy po³¹czona z przyciskiem za³aduj bazê, powoduje przejœcie do kolejnego widoku ³adowania bazy
        /// </summary>
        public RelayCommand LoadBaseCommand { get; set; }

        /// <summary>
        /// Deklaracja komendy wywo³ywanej podczas ³adowania widoku
        /// </summary>
        public RelayCommand OnLoad { get; set; }

        /// <summary>
        /// Konstruktor klasy, wywo³uje metode InitCommand i ustawia zmienn¹ nawigacyjn¹
        /// </summary>
        /// <param name="navService"></param>
        public MainViewModel(IMyNavigationService navService)
        {
            navigationService = navService;
            InitCommand();
        }

        /// <summary>
        /// Metoda zawieraj¹ca cia³a komend zadeklarowanych wczesniej
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