using GalaSoft.MvvmLight.Ioc;
using ProjektTechniki.Services;
using ProjektTechniki.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjektTechniki
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private MyNavigationService navService;

        public MainWindow()
        {
                
            InitializeComponent();
            ((MainViewModel)this.DataContext).ShowFirstView();
            Frame.LoadCompleted += (s, e) => UpdateFrameDataContext();
            Frame.DataContextChanged += (s, e) => UpdateFrameDataContext();
            
        }

        private void UpdateFrameDataContext()
        {
            Page view = (Page)Frame.Content;
            if(view!=null)
            {
                view.DataContext = Frame.DataContext;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
