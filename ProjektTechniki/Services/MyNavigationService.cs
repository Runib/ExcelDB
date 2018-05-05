using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ProjektTechniki.Services
{
    class MyNavigationService : IMyNavigationService
    {
        private Frame frame;

        public string CurrentPageKey { get; private set; }

        public MyNavigationService(Frame frame)
        {
            this.frame = frame;
        }

        public void GoBack()
        {
            throw new NotImplementedException();
        }

        public void NavigateTo(string pageKey)
        {
            var uri = Path.Combine("View",$"{pageKey}.xaml");
            frame.Navigate(new Uri(uri,UriKind.Relative));
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
